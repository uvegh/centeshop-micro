

using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Common;
using Shared.Library.Events;

namespace Ordering.Infrastructure.Data;

public class OrderingDbContext:DbContext
{

    private readonly DomainEventDispatcher _dispatcher;
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options, DomainEventDispatcher dispatcher) : base(options)
    {
        _dispatcher = dispatcher;


    }
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    public override async Task<int> SaveChangesAsync(CancellationToken ct) {

        var result = await base.SaveChangesAsync(ct);
        //get all changes in domain events 
        var aggregratesChanged = ChangeTracker.Entries<AggregateRoot>().Where(e => e.Entity.DomainEvents.Any()).Select(e => e.Entity).ToList();
        await _dispatcher.DispatchEventAsync(aggregratesChanged);
        return result;
        
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);
        //ignore domainevent interface on cnuilding model because its only for outr business logic
        modelBuilder.Ignore <List<IDomainEvent>>();
        base.OnModelCreating(modelBuilder);
    }
}
