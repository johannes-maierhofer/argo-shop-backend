using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests
{
    [Collection("Application")]
    public abstract class IntegrationTestBase
    {
        protected ITestOutputHelper Output { get; }

        protected IntegrationTestBase(ITestOutputHelper output)
        {
            Output = output;
            Testing.CurrentUserId = null;
        }
    }

    // data are reseeded once for all tests in the test class
    public abstract class QueryIntegrationTestBase : IntegrationTestBase, IClassFixture<ReseedDbFixture>
    {
        protected QueryIntegrationTestBase(ITestOutputHelper output) 
            : base(output)
        {
        }
    }

    public abstract class CommandIntegrationTestBase : IntegrationTestBase, IAsyncLifetime
    {
        protected CommandIntegrationTestBase(ITestOutputHelper output)
            : base(output)
        {
        }

        // data are reseeded before each test
        public async Task InitializeAsync()
        {
            this.Output.WriteLine("Reseed Db data");
            await Testing.ReseedSampleData();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
