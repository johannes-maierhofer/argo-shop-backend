using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Common.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductDetails
{
    public record GetProductDetailsQuery : IRequest<Result<ProductDetailsView>>
    {
        public int Id { get; set; }
    }

    public class QueryHandler : IRequestHandler<GetProductDetailsQuery, Result<ProductDetailsView>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<ProductDetailsView>> Handle(GetProductDetailsQuery query,
            CancellationToken cancellationToken)
        {
            var view = await _dbContext.Catalog.Products
                .ProjectTo<ProductDetailsView>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken: cancellationToken);

            return view == null
                ? Result.NotFound<ProductDetailsView>()
                : Result.Ok(view);
        }
    }
}