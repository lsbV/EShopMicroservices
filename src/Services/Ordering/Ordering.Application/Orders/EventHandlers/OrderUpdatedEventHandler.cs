namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedDomainEvent>
{
    public Task Handle(OrderUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Domain Event {DomainEvent} handled", notification.GetType().Name);
        logger.LogInformation("Order with Id: {OrderId} updated successfully", notification.Order.Id);
        return Task.CompletedTask;
    }
}