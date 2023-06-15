using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests
{
    [Collection("Application")]
    public abstract class TestBase
    {
        protected ITestOutputHelper Output { get; }

        protected TestBase(ITestOutputHelper output)
        {
            Output = output;
            Testing.CurrentUserId = null;
        }
    }

    // data are reseeded once for all tests in the test class
    public abstract class QueryTestBase : TestBase, IClassFixture<ReseedDbFixture>
    {
        protected QueryTestBase(ITestOutputHelper output) 
            : base(output)
        {
        }
    }

    public abstract class CommandTestBase : TestBase, IAsyncLifetime
    {
        protected CommandTestBase(ITestOutputHelper output)
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
