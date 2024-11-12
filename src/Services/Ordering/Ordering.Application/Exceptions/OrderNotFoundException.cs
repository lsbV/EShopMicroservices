using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions;

public class OrderNotFoundException(Guid orderId) : NotFoundException($"Order with id {orderId} is not found.");