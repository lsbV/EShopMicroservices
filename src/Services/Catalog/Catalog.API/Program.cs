
using Catalog.API.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddAutoMapper(typeof(Program));
var assemblies = new[] { typeof(Program).Assembly };
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assemblies);
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
var connectionString = builder.Configuration.GetConnectionString("Database")
    ?? throw new ArgumentException("Database connection string is required");
builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddValidatorsFromAssembly(assemblies[0]);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString);

var app = builder.Build();

app.UseExceptionHandler(_ => { });

app.MapCarter();

app.UseHealthChecks("/health",
    new HealthCheckOptions() { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });



await app.RunAsync();