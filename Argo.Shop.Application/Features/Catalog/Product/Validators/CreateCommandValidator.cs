using FluentValidation;

namespace Argo.Shop.Application.Features.Catalog.Product.Validators
{
    public class CreateTodoListCommandValidator : AbstractValidator<Create.Command>
    {
        public CreateTodoListCommandValidator()
        {
            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(256).WithMessage("Name must not exceed 256 characters.");
            RuleFor(v => v.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(256).WithMessage("Category must not exceed 256 characters.");
        }
    }
}
