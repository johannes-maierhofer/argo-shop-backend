using Argo.Shop.Application.Features;
using Argo.Shop.Application.Features.Catalog.Product;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Product
{
    public class GetDetailsTests : TestBase
    {
        public GetDetailsTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetDetails_KnownId_Success()
        {
            const int productId = 1;
            var result = await Testing.SendAsync(new GetDetails.Query
            {
                Id = productId
            });

            Assert.Equal(ResultStatus.Success, result.Status);
            Assert.Equal(productId, result.Data?.Id);
        }

        [Fact]
        public async Task GetDetails_NonExistingId_NotFound()
        {
            const int nonExistingProductId = -1;
            var result = await Testing.SendAsync(new GetDetails.Query
                {
                    Id = nonExistingProductId
                }
            );

            Assert.Equal(ResultStatus.NotFound, result.Status);
        }
    }
}
