namespace Ordering.Domain.Abstractions;

public interface IAggreate<T> : IAggregate, IEntity<T>
{
}

public interface IAggregate : IEntity
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    IDomainEvent[] ClearDomainEvents();
}
