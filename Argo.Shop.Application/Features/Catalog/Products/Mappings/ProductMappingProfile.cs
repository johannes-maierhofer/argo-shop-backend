using Argo.Shop.Application.Features.Catalog.Products.Models;
using Argo.Shop.Domain.Catalog.Products;
using AutoMapper;

namespace Argo.Shop.Application.Features.Catalog.Products.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // commands
            CreateMap<CreateProduct.Command, Product>();

            // view models
            CreateMap<Product, ProductListView>();
            CreateMap<Product, ProductDetailsView>();
        }
    }
}
