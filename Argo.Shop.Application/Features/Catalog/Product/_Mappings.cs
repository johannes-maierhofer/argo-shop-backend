using Argo.Shop.Application.Features.Catalog.Product.Models;
using AutoMapper;

namespace Argo.Shop.Application.Features.Catalog.Product
{
    using Domain.Catalog;

    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // commands
            CreateMap<Create.Command, Product>();

            // view models
            CreateMap<Domain.Catalog.Product, ProductDetailsView>();
        }
    }
}
