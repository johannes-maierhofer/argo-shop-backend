namespace Argo.Shop.IntegrationTests
{
    [CollectionDefinition("Application")]
    public class ApplicationCollection : ICollectionFixture<ApplicationFixture>
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApplicationFixture : IDisposable
    {
        public ApplicationFixture()
        {
            Testing.ConfigureApplication();
            Testing.EnsureDatabase();
            Testing.SeedSampleData();
        }

        public void Dispose()
        {
            // ... clean up test data from the database ...
        }
    }
}
