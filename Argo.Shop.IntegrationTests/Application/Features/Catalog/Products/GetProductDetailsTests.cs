using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductDetails;
using Argo.Shop.IntegrationTests.Testing;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Products
{
    public class GetProductDetailsTests : QueryTestBase
    {
        public GetProductDetailsTests(ApplicationFixture fixture, ITestOutputHelper output) 
            : base(fixture, output)
        {
        }

        [Fact]
        public async Task GetProductDetails_ShouldReturnData_ForKnownId()
        {
            const int productId = 1;

            using var scope = CreateScope();
            var result = await scope.SendAsync(new GetProductDetailsQuery
            {
                Id = productId
            });

            Assert.Equal(ResultStatus.Ok, result.Status);
            Assert.Equal(productId, result.Data?.Id);
        }

        [Fact]
        public async Task GetProductDetails_ShouldReturnNotFound_ForNonExistingId()
        {
            const int nonExistingProductId = -1;

            using var scope = CreateScope();
            var result = await scope.SendAsync(new GetProductDetailsQuery
                {
                    Id = nonExistingProductId
                }
            );

            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
