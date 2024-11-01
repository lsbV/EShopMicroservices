using Discount.Grpc;
using Discount.Grpc.Data;
using Discount.Grpc.Interceptors;
using Discount.Grpc.Services;
using Discount.Grpc.Validators;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder();
builder.Services.AddGrpc(options => options.Interceptors.Add<ValidationInterceptor>());
builder.Services.AddGrpcReflection();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

var connectionString = builder.Configuration.GetConnectionString("Database")
                       ?? throw new ArgumentException("Database connection string is not found.");
builder.Services.AddDbContext<DiscountDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddTransient<ValidationInterceptor>();
builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
    .AsImplementedInterfaces()
    .WithTransientLifetime());

builder.Services.AddHealthChecks()
    .AddSqlite(connectionString);



var app = builder.Build();
await app.ApplyMigrationAsync();

app.UseHealthChecks("/health",
    new HealthCheckOptions() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

app.MapGrpcService<DiscountService>();

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}

await app.RunAsync();