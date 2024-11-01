using Discount.Grpc.Models;
using FluentValidation;

namespace Discount.Grpc.Validators;

public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountRequestValidator()
    {
        RuleFor(x => x.Coupon).NotNull();
        RuleFor(x => x.Coupon.ProductName).NotEmpty();
        RuleFor(x => x.Coupon.Description).NotEmpty();
        RuleFor(x => x.Coupon.Amount).GreaterThan(0);
    }
}