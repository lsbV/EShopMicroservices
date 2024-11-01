using System.Diagnostics.CodeAnalysis;

namespace Discount.Grpc.Models;

public class Coupon
{
    public Coupon()
    {
    }

    [SetsRequiredMembers]
    public Coupon(int id, string productName, string description, int amount)
    {
        Id = id;
        ProductName = productName;
        Description = description;
        Amount = amount;
    }

    public required int Id { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public required int Amount { get; set; }
}