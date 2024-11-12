using Basket.API.Data;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Handlers;
using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddCarter();
services.AddAutoMapper(typeof(Program));
var assembly = typeof(Program).Assembly;
services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

var databaseConnectionString = builder.Configuration.GetConnectionString("Database")
    ?? throw new ArgumentException("Database connection string is required");
var redisConnectionString = builder.Configuration.GetConnectionString("Redis")
    ?? throw new ArgumentException("Redis connection string is required");
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
});
services.AddMarten(ops =>
    {
        ops.Connection(databaseConnectionString);
        ops.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();

builder.Services.AddMessageBroker(builder.Configuration);

services.AddExceptionHandler<CustomExceptionHandler>();
services.AddValidatorsFromAssembly(assembly);

services.AddHealthChecks()
    .AddNpgSql(databaseConnectionString)
    .AddRedis(redisConnectionString);

services.AddScoped<IBasketRepository, BasketRepository>();
services.Decorate<IBasketRepository, CachedBasketRepository>();

services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcConfigs:DiscountUrl"] ?? throw new ArgumentException("DiscountUrl is required"));
});






var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });
app.MapCarter();

app.MapHealthChecks("/health", new HealthCheckOptions() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });


await app.RunAsync();
