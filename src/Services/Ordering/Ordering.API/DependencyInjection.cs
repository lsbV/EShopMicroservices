using BuildingBlocks.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks()
.AddRabbitMQ(new Uri(configuration.GetSection("MessageBroker").Get<RabbitMqOptions>()?.Host ?? throw new ArgumentException("MessageBroker__Host")))
            .AddSqlServer(configuration.GetConnectionString("Database")
                          ?? throw new ArgumentException("Database connection string is required"));

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseExceptionHandler(_ => { });
        app.MapCarter();

        app.UseHealthChecks("/health",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

        return app;
    }

    private sealed class RabbitMqOptions
    {
        public string Host { get; init; } = default!;
        public string UserName { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}