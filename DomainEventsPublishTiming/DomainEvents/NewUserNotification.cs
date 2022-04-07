using MediatR;

namespace DomainEventsPublishTiming.DomainEvents
{
  public record NewUserNotification(string UserName, DateTime Time) : INotification;
}
