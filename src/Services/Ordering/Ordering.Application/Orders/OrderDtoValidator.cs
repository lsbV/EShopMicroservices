using FluentValidation;

namespace Ordering.Application.Orders;

public class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(order => order.CustomerId).NotEmpty();
        RuleFor(order => order.OrderName).NotEmpty().MaximumLength(100);
        RuleFor(order => order.ShippingAddress).SetValidator(new AddressDtoValidator());
        RuleFor(order => order.BillingAddress).SetValidator(new AddressDtoValidator());
        RuleFor(order => order.Payment).SetValidator(new PaymentDtoValidator());
        RuleFor(order => order.Status).IsInEnum();
        RuleFor(order => order.Items).NotEmpty();
    }
}


public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(address => address.Street).NotEmpty().MaximumLength(200);
        RuleFor(address => address.City).NotEmpty().MaximumLength(100);
        RuleFor(address => address.State).NotEmpty().MaximumLength(50);
        RuleFor(address => address.ZipCode).NotEmpty().MaximumLength(20);
    }
}

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(payment => payment.CardNumber).CreditCard();
        RuleFor(payment => payment.Expiration).NotEmpty().GreaterThan(DateTime.UtcNow);
        RuleFor(payment => payment.Cvv).NotEmpty().Length(3, 4);
    }
}

public class OrderItemDtoValidator : AbstractValidator<OrderItemDto>
{
    public OrderItemDtoValidator()
    {
        RuleFor(item => item.Quantity).GreaterThan(0);
        RuleFor(item => item.Price).GreaterThan(0);
        RuleFor(item => item.ProductId).NotEmpty();
    }
}

