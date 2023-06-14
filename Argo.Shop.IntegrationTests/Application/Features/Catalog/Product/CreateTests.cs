using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Product
{
    public class CreateTests : CommandIntegrationTestBase
    {
        public CreateTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task Create_ValidCommand_Ok()
        {
            var result = await Testing.SendAsync(new CreateProduct.Command
            {
                Name = "New shoes - " + Guid.NewGuid(),
                Category = "Shoes"
            });

            Assert.Equal(ResultStatus.Ok, result.Status);
            Assert.True(result.Data > 0);
        }

        [Fact]
        public async Task Create_RequiredValuesAreEmpty_Invalid()
        {
            var result = await Testing.SendAsync(new CreateProduct.Command());

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }

        [Fact]
        public async Task Create_WithExistingProductName_Invalid()
        {
            var result = await Testing.SendAsync(new CreateProduct.Command
            {
                Name = "Black Five-Panel Cap with White Logo", // product with this name already exists
                Category = "Caps"
            });

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }
    }
}
