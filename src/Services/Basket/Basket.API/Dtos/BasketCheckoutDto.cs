namespace Basket.API.Dtos;

public class BasketCheckoutDto
{
    public string UserName { get; set; } = string.Empty;
    public Guid CustomerId { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;
    public string CardHolderName { get; set; } = string.Empty;
    public DateTime CardExpiration { get; set; } = DateTime.UtcNow;
    public string Cvv { get; set; } = string.Empty;
    public int PaymentMethod { get; set; }
}