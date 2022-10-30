using Argo.Shop.Application.Features.Catalog.Product;
using Argo.Shop.Application.Features;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Application.Features.Catalog.Product
{
    public class CreateTests : TestBase
    {
        public CreateTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task Create_ValidCommand_Success()
        {
            var result = await Testing.SendAsync(new Create.Command
            {
                Name = "New shoes - " + Guid.NewGuid(),
                Category = "Shoes"
            });

            Assert.Equal(ResultStatus.Success, result.Status);
            Assert.True(result.Data > 0);
        }

        [Fact]
        public async Task Create_RequiredValuesAreEmpty_Invalid()
        {
            var result = await Testing.SendAsync(new Create.Command());

            Assert.Equal(ResultStatus.Invalid, result.Status);
            Assert.NotEmpty(result.Messages);
        }
    }
}
