using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products;
using Argo.Shop.Application.Features.Catalog.Products.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Argo.Shop.WebApi.Controllers.Catalog
{
    [ApiController]
    [Route("api/catalog/[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<PagedResult<ProductListView>>> GetList([FromQuery]GetProductList.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpGet("{id:int}")]
        public async Task<Result<ProductDetailsView>> GetDetails(int id)
        {
            return await _mediator.Send(new GetProductDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<Result<int>> Create(CreateProduct.Command cmd)
        {
            return await _mediator.Send(cmd);
        }
    }
}