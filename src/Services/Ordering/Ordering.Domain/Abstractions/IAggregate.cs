﻿namespace Ordering.Domain.Abstractions;

public interface IAggregate<T> : IAggregate, IEntity<T>;
public interface IAggregate
{
    IReadOnlyList<IDomainEvent> Events { get; }
    IDomainEvent[] FlushEvents();
}