using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductList;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Products
{
    public class GetProductListTests : QueryTestBase
    {
        public GetProductListTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task GetProductList_ShouldReturnNonEmptyList_WhenOk()
        {
            var result = await Testing.SendAsync(new GetProductListQuery());
            
            Assert.Equal(ResultStatus.Ok, result.Status);
            Assert.NotEmpty(result.Data?.Items!);
        }
    }
}
