using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductDetails;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Products
{
    public class GetProductDetailsTests : QueryIntegrationTestBase
    {
        public GetProductDetailsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetDetails_KnownId_Ok()
        {
            const int productId = 1;
            var result = await Testing.SendAsync(new GetProductDetailsQuery
            {
                Id = productId
            });

            Assert.Equal(ResultStatus.Ok, result.Status);
            Assert.Equal(productId, result.Data?.Id);
        }

        [Fact]
        public async Task GetDetails_NonExistingId_NotFound()
        {
            const int nonExistingProductId = -1;
            var result = await Testing.SendAsync(new GetProductDetailsQuery
                {
                    Id = nonExistingProductId
                }
            );

            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
