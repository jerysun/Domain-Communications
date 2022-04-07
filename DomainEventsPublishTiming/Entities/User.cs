using DomainEventsPublishTiming.DomainEvents;
using DomainEventsPublishTiming.Helpers;

namespace DomainEventsPublishTiming.Entities
{
  public class User : BaseEntity
  {
    public int Id { get; init; }
    public DateTime CreateDateTime { get; init; }
    public string UserName { get; private set; } = string.Empty;
    public int Credits { get; init; }

    private string? passwordHash;
    private string? remark;
    public string? Remark { get { return this.Remark; } }
    public string? Tag { get; init; }

    private User() { }//给EFCore从数据库中加载数据然后生成User对象返回用的

    public User(string yhm) // for developers
    {
      this.UserName = yhm;
      this.CreateDateTime = DateTime.UtcNow;
      this.Credits = 10;
      // Event registration for publishing new user
      AddDomainEvent(new NewUserNotification(UserName, CreateDateTime));
    }

    public void ChangeUserName(string un)
    {
      if (un.Length > 5)
      {
        Console.WriteLine("The length of user name cannot be longer than 5");
        return;
      }

      // Event registration for changing user name
      AddDomainEvent(new UserNameChangeNotification(UserName, un));
      this.UserName = un;
    }

    public void ChangePassword(string pwd)
    {
      if (pwd.Length < 3)
      {
        Console.WriteLine("The length of password cannot be less than 3");
        return;
      }
      this.passwordHash = HashHelper.GetMd5Hash(pwd);
    }
  }
}
