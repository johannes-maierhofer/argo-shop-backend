using Argo.Shop.Application.Common.Persistence;
using AutoMapper;
using MediatR;

namespace Argo.Shop.Application.Features.Catalog.Product
{
    using Domain.Catalog;

    public static class Create
    {
        public class Command : CommandBase<Result<int>>
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

                return Result.Success(product.Id);
            }
        }
    }
}
