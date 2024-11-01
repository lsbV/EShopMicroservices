using BuildingBlocks.Exceptions;

namespace Basket.API.Infrastructure.Exceptions;

public class BasketNotFoundException(string userName) : NotFoundException($"Basket not found for user: {userName}");