namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsDeleted);

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}

public class DeleteOrderHandler(IApplicationDbContext context)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await context.Orders.FindAsync([OrderId.Of(command.OrderId)], cancellationToken)
            ?? throw new OrderNotFoundException(command.OrderId);
        context.Orders.Remove(order);

        await context.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}