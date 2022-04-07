using MediatR;

namespace DomainEventsPublishTiming.DomainEvents
{
  public abstract class BaseEntity : IDomainEvents
  {
    private readonly IList<INotification> _events = new List<INotification>();

    public void AddDomainEvent(INotification eventItem) => _events.Add(eventItem);

    public void ClearDomainEvents() => _events.Clear();

    public IEnumerable<INotification> GetDomainEvents() => _events;
  }
}
