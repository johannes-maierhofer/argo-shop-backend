// ReSharper disable ClassNeverInstantiated.Global
namespace Argo.Shop.IntegrationTests
{
    [CollectionDefinition("Application")]
    public class ApplicationCollection : ICollectionFixture<ApplicationFixture>
    {
    }

    public class ApplicationFixture : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            Testing.ConfigureApplication();
            await Testing.EnsureDatabase();
            await Testing.SeedSampleData();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }

    public class ReseedDbFixture : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            await Testing.ReseedSampleData();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
