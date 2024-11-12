namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultMaxLength = 15;
    public string Value { get; init; }

    private OrderName(string value) => Value = value;

    public static OrderName Of(string value, int maxLength = DefaultMaxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"{nameof(OrderName)} cannot be empty");
        }
        if (value.Length > maxLength)
        {
            throw new DomainException($"{nameof(OrderName)} cannot be longer than {maxLength}");
        }
        return new OrderName(value);
    }
}