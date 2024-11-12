namespace Ordering.Domain.Abstractions;

public abstract class Aggregate<TId>(TId id) : Entity<TId>(id), IAggregate<TId>
{
    private readonly List<IDomainEvent> _events = [];
    public IReadOnlyList<IDomainEvent> Events => _events.AsReadOnly();

    public IDomainEvent[] FlushEvents()
    {
        var events = _events.ToArray();
        _events.Clear();
        return events;
    }

    protected void AddDomainEvent(IDomainEvent @event)
    {
        _events.Add(@event);
    }
}