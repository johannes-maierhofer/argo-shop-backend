using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Common.Persistence;
using Argo.Shop.Application.Features.Catalog.Products.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Argo.Shop.Application.Features.Catalog.Products
{
    public static class GetProductList
    {
        public record Query : IRequest<Result<PagedResult<ProductListView>>>
        {
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 3;
            public string? FilterText { get; set; } = null;
        }

        public class QueryHandler : IRequestHandler<Query, Result<PagedResult<ProductListView>>>
        {
            private readonly IAppDbContext _dbContext;
            private readonly IMapper _mapper;

            public QueryHandler(IAppDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<Result<PagedResult<ProductListView>>> Handle(Query query, CancellationToken cancellationToken)
            {
                var dbQuery = _dbContext.Catalog.Products
                    .WhereIf(!string.IsNullOrEmpty(query.FilterText),
                        p => p.Name.Contains(query.FilterText!) ||
                             (p.Description != null && p.Description.Contains(query.FilterText!)))
                    .ProjectTo<ProductListView>(_mapper.ConfigurationProvider);

                var pagedResult = await dbQuery.ToPagedResult(query.Page, query.PageSize);

                return Result.Ok(pagedResult);
            }
        }
    }
}
