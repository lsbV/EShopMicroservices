using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extension
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            if (assembly is not null)
            {
                x.AddConsumers(assembly);
            }

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(configuration["MessageBroker:Host"] ?? throw new ArgumentException("Host for MessageBroker is required")), h =>
                {
                    h.Username(configuration["MessageBroker:UserName"] ?? throw new ArgumentException("Username for MessageBroker is required"));
                    h.Password(configuration["MessageBroker:Password"] ?? throw new ArgumentException("Password for MessageBroker is required"));
                });
                cfg.ConfigureEndpoints(context);
            });
        });
        return services;
    }
}