using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Argo.Shop.Application.Common.Persistence;
using Argo.Shop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Argo.Shop.Domain.Catalog.Products;

namespace Argo.Shop.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ILoggerFactory loggerFactory) 
            : base(options)
        {
            _loggerFactory = loggerFactory;
            this.Catalog = new CatalogSchema(this);
        }

        public ICatalogSchema Catalog { get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
            // optionsBuilder.UseLazyLoadingProxies();
            // optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution);
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }

    public class CatalogSchema : ICatalogSchema
    {
        private readonly AppDbContext _dbContext;

        public CatalogSchema(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<Product> Products => _dbContext.Set<Product>();
        public DbSet<ProductImage> ProductImages => _dbContext.Set<ProductImage>();
    }
}
