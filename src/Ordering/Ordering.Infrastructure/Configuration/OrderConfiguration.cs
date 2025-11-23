

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Configuration;


public class OrderConfiguration:IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> modelBuilder) {
        modelBuilder.ToTable("Orders");
        modelBuilder.Property(p => p.UserId).IsRequired();
        modelBuilder.HasMany(p => p.Items).WithOne().HasForeignKey(o => o.OrderId).OnDelete(DeleteBehavior.Cascade);


        
        modelBuilder.Property(p => p.Status).IsRequired();
        modelBuilder.Property(p => p.TotalAmount).HasColumnType("decimal").IsRequired();
        modelBuilder.Property(p => p.Address).IsRequired();
        //set items to field so it can be accessed since its private
        modelBuilder.Metadata.FindNavigation(nameof(Order.Items))!.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
