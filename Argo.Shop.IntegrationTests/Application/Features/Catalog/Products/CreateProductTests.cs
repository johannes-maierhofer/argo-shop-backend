using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products.Commands.CreateProduct;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Products
{
    public class CreateProductTests : CommandIntegrationTestBase
    {
        public CreateProductTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task Create_ValidCommand_Ok()
        {
            Testing.RunAsAdministrator();
            var result = await Testing.SendAsync(new CreateProductCommand
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
            Testing.RunAsAdministrator();
            var result = await Testing.SendAsync(new CreateProductCommand());

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }

        [Fact]
        public async Task Create_WithExistingProductName_Invalid()
        {
            Testing.RunAsAdministrator();

            var result = await Testing.SendAsync(new CreateProductCommand
            {
                Name = "Black Five-Panel Cap with White Logo", // product with this name already exists
                Category = "Caps"
            });

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }
    }
}
