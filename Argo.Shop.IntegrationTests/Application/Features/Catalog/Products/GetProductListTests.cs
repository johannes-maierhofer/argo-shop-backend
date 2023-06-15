using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductList;
using Argo.Shop.IntegrationTests.Testing;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Products
{
    public class GetProductListTests : QueryTestBase
    {
        public GetProductListTests(ApplicationFixture fixture, ITestOutputHelper output) 
            : base(fixture, output)
        {
        }

        [Fact]
        public async Task GetProductList_ShouldReturnNonEmptyList_WhenOk()
        {
            using var scope = CreateScope();
            var result = await scope.SendAsync(new GetProductListQuery());
            
            Assert.Equal(ResultStatus.Ok, result.Status);
            Assert.NotEmpty(result.Data?.Items!);
        }
    }
}
