using Argo.Shop.Application.Features;
using Argo.Shop.Application.Features.Catalog.Product;
using Argo.Shop.Application.Features.Catalog.Product.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Argo.Shop.WebApi.Controllers.Catalog
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<ProductDetailsView>> GetDetails(int id)
        {
            return await _mediator.Send(new GetDetails.Query { Id = id });
        }

        [HttpPost]
        public async Task<Result<int>> Create(Create.Command cmd)
        {
            return await _mediator.Send(cmd);
        }
    }
}