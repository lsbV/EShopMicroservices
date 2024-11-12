namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event {DomainEvent} handled", notification.GetType().Name);
        logger.LogInformation("Order with Id: {OrderId} created successfully", notification.Order.Id);
        return Task.CompletedTask;

    }
}