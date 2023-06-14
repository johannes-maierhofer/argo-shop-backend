using Argo.Shop.Application.Common.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Argo.Shop.Application.Features.Catalog.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IAppDbContext _dbContext;

        public CreateProductCommandValidator(IAppDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(v => v.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(256).WithMessage("Name must not exceed 256 characters.")
                .MustAsync(BeUniqueName).WithMessage("The specified product name already exists.");

            RuleFor(v => v.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(256).WithMessage("Category must not exceed 256 characters.");
        }

        private async Task<bool> BeUniqueName(string productName, CancellationToken cancellationToken)
        {
            return await _dbContext.Catalog.Products
                .AllAsync(l => l.Name != productName, cancellationToken);
        }
    }
}
