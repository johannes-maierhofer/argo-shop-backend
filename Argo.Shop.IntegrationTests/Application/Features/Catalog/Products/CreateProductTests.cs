using Argo.Shop.Application.Common.Models;
using Argo.Shop.Application.Features.Catalog.Products.Commands.CreateProduct;
using Argo.Shop.IntegrationTests.Testing;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Products
{
    public class CreateProductTests : CommandTestBase
    {
        public CreateProductTests(ApplicationFixture fixture, ITestOutputHelper output) 
            : base(fixture, output)
        {
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnOk_WhenCommandIsValid()
        {
            using var scope = CreateScopeForAdminUser();
            var result = await scope.SendAsync(new CreateProductCommand
            {
                Name = "New shoes - " + Guid.NewGuid(),
                Category = "Shoes"
            });

            Assert.Equal(ResultStatus.Ok, result.Status);
            Assert.True(result.Data > 0);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnUnauthorized_WhenCurrentUserIsNotAdministrator()
        {
            using var scope = CreateScopeForDefaultUser();
            var result = await scope.SendAsync(new CreateProductCommand
            {
                Name = "New shoes - " + Guid.NewGuid(),
                Category = "Shoes"
            });

            Assert.Equal(ResultStatus.Unauthorized, result.Status);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnInvalid_WhenRequiredValuesAreEmpty()
        {
            using var scope = CreateScopeForAdminUser();
            var result = await scope.SendAsync(new CreateProductCommand());

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }

        [Fact]
        public async Task CreateProduct_ShouldReturnInvalid_WhenProductNameAlreadyExists()
        {
            using var scope = CreateScopeForAdminUser();

            var result = await scope.SendAsync(new CreateProductCommand
            {
                Name = "Black Five-Panel Cap with White Logo", // product with this name already exists
                Category = "Caps"
            });

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }
    }
}
