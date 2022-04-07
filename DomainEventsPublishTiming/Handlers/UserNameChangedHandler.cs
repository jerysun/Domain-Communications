using DomainEventsPublishTiming.DomainEvents;
using MediatR;

namespace DomainEventsPublishTiming.Handlers
{
  public class UserNameChangedHandler : NotificationHandler<UserNameChangeNotification>
  {
    protected override void Handle(UserNameChangeNotification notification)
    {
      Console.WriteLine($"The user name is changed from {notification.OldUserName} to {notification.NewUserName}");
    }
  }
}
