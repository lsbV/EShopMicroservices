using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

internal class CouponRepository(DiscountDbContext context)
    : ICouponRepository
{
    public async Task<Coupon?> GetCoupon(string productName)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.ProductName == productName);
        return coupon;
    }

    public async Task<bool> CreateCoupon(Coupon coupon)
    {
        await context.Coupons.AddAsync(coupon);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateCoupon(Coupon coupon)
    {
        var existingCoupon = await context.Coupons.FindAsync(coupon.Id);
        if (existingCoupon == null)
        {
            return false;
        }
        existingCoupon.ProductName = coupon.ProductName;
        existingCoupon.Amount = coupon.Amount;
        existingCoupon.Description = coupon.Description;
        context.Coupons.Update(existingCoupon);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCoupon(string productName)
    {
        var coupon = await context.Coupons.FirstOrDefaultAsync(c => c.ProductName == productName);
        if (coupon == null)
        {
            return false;
        }
        context.Coupons.Remove(coupon);
        await context.SaveChangesAsync();
        return true;
    }
}