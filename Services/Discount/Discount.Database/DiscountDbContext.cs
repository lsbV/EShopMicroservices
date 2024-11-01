using Discount.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Database;

public class DiscountDbContext(DbContextOptions<DiscountDbContext> options) : DbContext(options)
{
    public required DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Id).ValueGeneratedOnAdd();
            e.Property(c => c.Amount).IsRequired();
            e.Property(c => c.Description).IsRequired();
        });
    }
}