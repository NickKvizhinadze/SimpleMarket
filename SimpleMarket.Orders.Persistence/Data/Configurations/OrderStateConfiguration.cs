using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMarket.Orders.Persistence.Saga;

namespace SimpleMarket.Orders.Persistence.Data.Configurations;

public class OrderStateConfiguration : IEntityTypeConfiguration<OrderStateInstance>
{
    public void Configure(EntityTypeBuilder<OrderStateInstance> builder)
    {
        builder.HasKey(x => x.OrderId);
        builder.Property(x => x.CurrentState)
            .HasMaxLength(64);
        builder.Property(x => x.CustomerId)
            .HasMaxLength(240);
        builder.Property(x => x.PaymentAccountId)
            .HasMaxLength(240);
    }
}