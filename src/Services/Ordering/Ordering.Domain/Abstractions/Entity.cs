namespace Ordering.Domain.Abstractions;

public abstract class Entity<T>(T id) : IEntity<T>
{
    public T Id { get; init; } = id;
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string? LastModifiedBy { get; set; }
}