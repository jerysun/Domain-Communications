using DomainEventsPublishTiming.DomainEvents;
using DomainEventsPublishTiming.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DomainEventsPublishTiming.Data
{
  public class DomainEventsDbContext : DbContext
  {
    private readonly IMediator _mediator;
    public DbSet<User> Users { get; set; }

    public DomainEventsDbContext(DbContextOptions<DomainEventsDbContext> options, IMediator mediator) : base(options)
    {
      _mediator = mediator;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      /*
       base.OnModelCreating(modelBuilder);
       modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
       */
      modelBuilder.ApplyConfiguration(new UserConfig());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      var domainEntities = this.ChangeTracker.Entries<IDomainEvents>()
        .Where(x => x.Entity.GetDomainEvents().Any());//If DomainEvents is empty, then we shouldn't proceed
      // ToList() is to prevent the lazy loading, otherwise the following statement
      // will clear them, because ToList() is to get a deep copy.
      var domainEvents = domainEntities.SelectMany(x => x.Entity.GetDomainEvents()).ToList();

      // The copy of domain events is made, so we can and should clear them to prevent from repeating publish
      domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());
      foreach(var domainEvent in domainEvents)
      {
        await _mediator.Publish(domainEvent);
      }

      return await base.SaveChangesAsync(cancellationToken);
    }
  }
}
