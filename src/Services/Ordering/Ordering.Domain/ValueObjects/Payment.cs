namespace Ordering.Domain.ValueObjects;

public class Payment
{
    public string CardNumber { get; private set; }
    public string CardHolderName { get; private set; }
    public DateTime Expiration { get; private set; }
    public string SecurityNumber { get; private set; }
    public int PaymentMethod { get; private set; }

    protected Payment()
    {
        CardNumber = string.Empty;
        CardHolderName = string.Empty;
        Expiration = DateTime.MinValue;
        SecurityNumber = string.Empty;
        PaymentMethod = 0;
    }

    public static Payment Of(string cardNumber, string cardHolderName, DateTime expiration, string securityNumber,
        int paymentMethod)
    {
        //TODO: Add validation
        var payment = new Payment()
        {
            CardNumber = cardNumber,
            CardHolderName = cardHolderName,
            Expiration = expiration,
            SecurityNumber = securityNumber,
            PaymentMethod = paymentMethod
        };

        return payment;
    }

}