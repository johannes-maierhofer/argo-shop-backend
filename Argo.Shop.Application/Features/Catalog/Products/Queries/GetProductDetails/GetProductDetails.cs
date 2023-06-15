using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Common.Persistence;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductDetails
{
    public record GetProductDetailsQuery : IRequest<Result<ProductDetailsDto>>
    {
        public int Id { get; set; }
    }

    public class QueryHandler : IRequestHandler<GetProductDetailsQuery, Result<ProductDetailsDto>>
    {
        private readonly IAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public QueryHandler(IAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<ProductDetailsDto>> Handle(GetProductDetailsQuery query,
            CancellationToken cancellationToken)
        {
            var dto = await _dbContext.Catalog.Products
                .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(p => p.Id == query.Id, cancellationToken: cancellationToken);

            return dto == null
                ? Result.NotFound<ProductDetailsDto>()
                : Result.Ok(dto);
        }
    }
}