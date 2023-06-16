using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Argo.Shop.WebApi.Configuration
{
    public static class BearerTokenConfigurationExtensions
    {
        public static IServiceCollection AddBearerTokenAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSection = configuration.GetSection("JwtBearerToken");
            services.Configure<JwtBearerTokenSettings>(jwtSection);
            var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>() ?? new JwtBearerTokenSettings();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtBearerTokenSettings.Issuer,
                        ValidAudience = jwtBearerTokenSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                    };
                });

            return services;
        }
    }
}
