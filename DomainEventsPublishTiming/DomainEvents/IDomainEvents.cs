using MediatR;

namespace DomainEventsPublishTiming.DomainEvents
{
  public interface IDomainEvents
  {
    IEnumerable<INotification> GetDomainEvents();
    void AddDomainEvent(INotification eventItem);
    //void AddDomainEventIfAbsent(INotification eventItem);
    void ClearDomainEvents();
  }
}
