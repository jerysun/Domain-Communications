using MediatR;

namespace DomainEventsPublishTiming.DomainEvents
{
  public record UserNameChangeNotification(string OldUserName, string NewUserName) : INotification;
}
