using FluentValidation;

namespace Discount.Grpc.Validators;

public class DeleteDiscountRequestValidator : AbstractValidator<DeleteDiscountRequest>
{
    public DeleteDiscountRequestValidator()
    {
        RuleFor(x => x.ProductName).NotEmpty();
    }
}