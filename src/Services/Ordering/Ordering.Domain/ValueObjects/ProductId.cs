namespace Ordering.Domain.ValueObjects;

public record ProductId
{
    public Guid Value { get; init; }

    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException($"{nameof(ProductId)} cannot be empty");
        }
        return new ProductId(value);
    }
}