using BuildingBlocks.Behaviors;
using BuildingBlocks.Handlers;
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
services.AddHealthChecks();
services.AddExceptionHandler<CustomExceptionHandler>();
services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });
app.MapCarter();

app.MapHealthChecks("/health", new HealthCheckOptions() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });


await app.RunAsync();
