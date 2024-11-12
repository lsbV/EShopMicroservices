namespace BuildingBlocks.Messaging.Events;

public record BasketCheckoutEvent(
    string UserName,
    Guid CustomerId,
    string FirstName,
    string LastName,
    string City,
    string Street,
    string State,
    string Country,
    string ZipCode,
    string CardNumber,
    string CardHolderName,
    DateTime CardExpiration,
    string Cvv,
    int PaymentMethod,
    decimal TotalPrice)
    : IntegrationEvent
{
    public BasketCheckoutEvent() : this(string.Empty, Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, DateTime.UtcNow, string.Empty, 0, 0)
    {
    }
}