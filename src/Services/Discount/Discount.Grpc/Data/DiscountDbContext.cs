using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public required DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Coupon>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();
            e.Property(c => c.Amount).IsRequired();
            e.Property(c => c.Description).IsRequired();
        });

        modelBuilder.Entity<Coupon>().HasData(
            new Coupon(id: 1, productName: "Keyboard", description: "Keyboard discount", amount: 6),
            new Coupon(id: 2, productName: "Mouse", description: "Mouse discount", amount: 4)
        );
    }
}