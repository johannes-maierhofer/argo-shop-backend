using Argo.Shop.Application.Features.Catalog.Products.Commands.CreateProduct;
using Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductDetails;
using Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductList;
using Argo.Shop.Domain.Catalog.Products;
using AutoMapper;

namespace Argo.Shop.Application.Features.Catalog.Products.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            // commands
            CreateMap<CreateProductCommand, Product>();

            // view models
            CreateMap<Product, ProductListDto>();
            CreateMap<Product, ProductDetailsDto>();
            CreateMap<ProductImage, ProductDetailsDto.ProductImageListDto>();
        }
    }
}
