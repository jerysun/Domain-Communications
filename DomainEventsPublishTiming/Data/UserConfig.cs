using DomainEventsPublishTiming.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomainEventsPublishTiming.Data
{
  public class UserConfig : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.ToTable("Users");
      builder.Property("UserName").HasMaxLength(32);
      builder.Property("passwordHash").HasMaxLength(64); //特征3
      builder.Property(x => x.Remark).HasField("remark").HasMaxLength(512); //特征4. HasField("remark") apparently uses REFLECTION
      builder.Ignore(x => x.Tag); //特征5
    }
  }
}
