using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);

        builder.OwnsOne(o => o.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName)
                .HasColumnName("Shipping_FirstName")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.LastName)
                .HasColumnName("Shipping_LastName")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.Street)
                .HasColumnName("Shipping_Street")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.City)
                .HasColumnName("Shipping_City")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.State)
                .HasColumnName("Shipping_State")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasColumnName("Shipping_Country")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.ZipCode)
                .HasColumnName("Shipping_ZipCode")
                .HasMaxLength(18)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.FirstName)
                .HasColumnName("Billing_FirstName")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.LastName)
                .HasColumnName("Billing_LastName")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.Street)
                .HasColumnName("Billing_Street")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.City)
                .HasColumnName("Billing_City")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.State)
                .HasColumnName("Billing_State")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.Country)
                .HasColumnName("Billing_Country")
                .HasMaxLength(100)
                .IsRequired();

            addressBuilder.Property(a => a.ZipCode)
                .HasColumnName("Billing_ZipCode")
                .HasMaxLength(18)
                .IsRequired();
        });

        builder.ComplexProperty(o => o.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardHolderName)
                .HasColumnName(nameof(Payment.CardHolderName))
                .HasMaxLength(50)
                .IsRequired();

            paymentBuilder.Property(p => p.CardNumber)
                .HasColumnName(nameof(Payment.CardNumber))
                .HasMaxLength(24)
                .IsRequired();

            paymentBuilder.Property(p => p.Expiration)
                .HasColumnName(nameof(Payment.Expiration))
                .IsRequired();

            paymentBuilder.Property(p => p.SecurityNumber)
                .HasColumnName(nameof(Payment.SecurityNumber))
                .HasMaxLength(3)
                .IsRequired();
        });

        builder.Property(o => o.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                status => status.ToString(),
                dbStatus => Enum.Parse<OrderStatus>(dbStatus)
            );
    }
}
