using Catalog.API.Infrastructure;
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct;

public record UpdateProductCommand(Product Product) : ICommand<Unit>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product).NotNull().WithMessage("Product is required.");
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Product Id is required.");
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
        RuleFor(x => x.Product.Categories).NotEmpty().WithMessage("Categories is required.");
        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
    }
}


public class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand>
{
    async Task<Unit> IRequestHandler<UpdateProductCommand, Unit>.Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Product.Id, cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }
        session.Update<Product>(command.Product);
        await session.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}