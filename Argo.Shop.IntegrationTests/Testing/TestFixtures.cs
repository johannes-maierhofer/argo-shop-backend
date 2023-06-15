using Argo.Shop.Infrastructure.Persistence;
using Argo.Shop.IntegrationTests.Testing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;

namespace Argo.Shop.IntegrationTests.Testing
{
    [CollectionDefinition("Application")]
    public class ApplicationCollection : ICollectionFixture<ApplicationFixture>
    {
    }

    // ReSharper disable once ClassNeverInstantiated.Global
    public class ApplicationFixture : IAsyncLifetime
    {
        private IConfiguration _configuration = null!;
        private Respawner _respawner = null!;

        public IServiceScopeFactory ScopeFactory { get; private set; } = null!;

        public async Task InitializeAsync()
        {
            var factory = new TestWebApplicationFactory();

            ScopeFactory = factory
                .Services
                .GetRequiredService<IServiceScopeFactory>();
            
            _configuration = factory
                .Services
                .GetRequiredService<IConfiguration>();

            await EnsureDatabaseCreated();
            await SeedSampleData();
        }

        public async Task DisposeAsync()
        {
            await EnsureDatabaseDeleted();
        }

        public async Task ReseedSampleData()
        {
            // deletes existing data according to RespawnerOptions
            await _respawner.ResetAsync(_configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
            await SeedSampleData();
        }

        private async Task EnsureDatabaseDeleted()
        {
            using var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.EnsureDeletedAsync();
        }

        private async Task EnsureDatabaseCreated()
        {
            await EnsureDatabaseDeleted();

            using var scope = ScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();

            // respawner creates a data delete script
            _respawner = await Respawner.CreateAsync(
                _configuration.GetConnectionString("DefaultConnection") ?? string.Empty,
                new RespawnerOptions
                {
                    TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
                });

            // in the future we might also check out Reseed from https://github.com/uladz-zubrycki/Reseed
        }

        private async Task SeedSampleData()
        {
            await DbContextSeed.SeedSampleData(ScopeFactory);
        }
    }

    public class ReseedDbFixture : IAsyncLifetime
    {
        private readonly ApplicationFixture _applicationFixture;

        public ReseedDbFixture(ApplicationFixture applicationFixture)
        {
            _applicationFixture = applicationFixture;
        }

        public async Task InitializeAsync()
        {
            await _applicationFixture.ReseedSampleData();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}
