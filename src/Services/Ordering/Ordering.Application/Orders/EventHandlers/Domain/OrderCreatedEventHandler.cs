using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain;

public class OrderCreatedEventHandler
    (ILogger<OrderCreatedEventHandler> logger, IFeatureManager featureManager, IPublishEndpoint publishEndpoint)
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event {DomainEvent} handled", notification.GetType().Name);

        var orderCreatedIntegrationEvent = notification.Order.ToOrderDto();

        if (await featureManager.IsEnabledAsync("OrderFulfillment"))
            await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
    }
}