using Argo.Shop.Application.Common.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Argo.Shop.Application.Common.Persistence;
using Argo.Shop.Application.Common.Services;
using Argo.Shop.Infrastructure.Identity;
using Argo.Shop.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Argo.Shop.Domain.Common.Events;
using Argo.Shop.Infrastructure.Services;
using Argo.Shop.Infrastructure.Persistence.Interceptors;

namespace Argo.Shop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // services.AddScoped<AuditableEntitiesInterceptor>();
            services.AddSingleton<DomainEventDispatchingInterceptor>();

            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));

                    // add interceptors
                    // options.AddInterceptors(serviceProvider.GetRequiredService<AuditableEntitiesInterceptor>());
                    options.AddInterceptors(serviceProvider.GetRequiredService<DomainEventDispatchingInterceptor>());
                }
            );

            services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());

            // Setup Identity
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                })
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();


            // TODO: add specific policies
            //services.AddAuthorization(options =>
            //    options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")));

            services.AddSingleton<IDomainEventPublisher, DomainEventPublisher>();
            services.AddTransient<IDateTimeService, DateTimeService>();

            //services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();

            return services;
        }

        
    }
}