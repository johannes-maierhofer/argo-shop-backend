using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Common.Persistence;
using Argo.Shop.Application.Common.Security;
using Argo.Shop.Domain.Catalog.Products;
using AutoMapper;
using MediatR;
using ReviewCleanArch.Application.Common.Mediatr;

namespace Argo.Shop.Application.Features.Catalog.Products
{
    public static class CreateProduct
    {
        [Authorize(Roles = "Administrator")]
        public class Command : ICommand<Result<int>>
        {
            public string Name { get; set; } = string.Empty;
            public decimal Price { get; set; }
            public string Category { get; set; } = string.Empty;
            public string? Description { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, Result<int>>
        {
            private readonly IAppDbContext _dbContext;
            private readonly IMapper _mapper;

            public CommandHandler(IAppDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<Result<int>> Handle(Command cmd, CancellationToken cancellationToken)
            {
                var product = _mapper.Map<Product>(cmd);
                _dbContext.Catalog.Products.Add(product);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result.Ok(product.Id);
            }
        }
    }
}
