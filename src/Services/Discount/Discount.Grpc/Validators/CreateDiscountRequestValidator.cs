using FluentValidation;

namespace Discount.Grpc.Validators;

public class CreateDiscountRequestValidator : AbstractValidator<CreateDiscountRequest>
{
    public CreateDiscountRequestValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
    }

}