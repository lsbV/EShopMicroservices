using System.Reflection;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var basketCheckoutEvent = context.Message;
        logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", basketCheckoutEvent.Id, Assembly.GetExecutingAssembly().FullName, basketCheckoutEvent);

        var command = basketCheckoutEvent.ToCreateOrderCommand();

        await sender.Send(command, context.CancellationToken);
    }
}