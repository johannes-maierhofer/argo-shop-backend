using Argo.Shop.IntegrationTests.Testing.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace Argo.Shop.IntegrationTests.Testing
{
    [Collection("Application")]
    public abstract class TestBase
    {
        protected readonly ApplicationFixture Fixture;
        protected ITestOutputHelper Output { get; }

        protected TestBase(ApplicationFixture fixture, ITestOutputHelper output)
        {
            Fixture = fixture;
            Output = output;
        }

        protected TestScope CreateScopeForDefaultUser()
        {
            return CreateScope(UserData.DefaultUserId);
        }

        protected TestScope CreateScopeForAdminUser()
        {
            return CreateScope(UserData.AdminUserId);
        }

        protected TestScope CreateScope(string? currentUserId = null)
        {
            var serviceScope = Fixture.ScopeFactory.CreateScope();
            var testScope = new TestScope(serviceScope, Output, currentUserId);

            var testScopeAccessor = serviceScope
                .ServiceProvider
                .GetRequiredService<TestScopeAccessor>();
            testScopeAccessor.TestScope = testScope;

            return testScope;
        }
    }

    // data are reseeded once for all tests in the test class
    public abstract class QueryTestBase : TestBase, IClassFixture<ReseedDbFixture>
    {
        protected QueryTestBase(ApplicationFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }
    }

    public abstract class CommandTestBase : TestBase, IAsyncLifetime
    {
        protected CommandTestBase(ApplicationFixture fixture, ITestOutputHelper output)
            : base(fixture, output)
        {
        }

        // data are reseeded before each test
        public async Task InitializeAsync()
        {
            Output.WriteLine("Reseed Db data");
            await Fixture.ReseedSampleData();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
