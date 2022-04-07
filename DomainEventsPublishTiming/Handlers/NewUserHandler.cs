using DomainEventsPublishTiming.DomainEvents;
using MediatR;

namespace DomainEventsPublishTiming.Handlers
{
  public class NewUserHandler : NotificationHandler<NewUserNotification>
  {
    protected override void Handle(NewUserNotification notification)
    {
      Console.WriteLine($"The newly created user: {notification.UserName}");
    }
  }
}
