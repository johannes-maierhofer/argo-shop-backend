using Argo.Shop.Application;
using Argo.Shop.Application.Common.Identity;
using Argo.Shop.Infrastructure;
using Argo.Shop.Infrastructure.Identity;
using Argo.Shop.Infrastructure.Persistence;
using Argo.Shop.WebApi.Configuration;
using Argo.Shop.WebApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// set up application
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();

builder.Services
    .AddBearerTokenAuthentication(builder.Configuration)
    .AddAuthorization();

//builder.Services.AddHealthChecks()
//    .AddDbContextCheck<AppDbContext>();

// setup CORS
const string corsPolicyName = "ArgoShop_AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, cfg => cfg
            .WithOrigins("http://localhost:4200",
                "https://localhost:4200", 
                "http://localhost:4300", 
                "https://localhost:4300")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
    );
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ArgoShop API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });

    // fixes problem with classes in namespaces
    options.CustomSchemaIds(type => type.ToString().Replace("+", "."));

    // for documentation to work, make sure the "Documentation file" option in the project properties is checked
    //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    //options.IncludeXmlComments(xmlPath, true);  //
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // setup virtual directory for images
    string physicalFilePath = Directory.GetParent(Directory.GetCurrentDirectory())!.FullName + "\\images";
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(physicalFilePath),
        RequestPath = new PathString("/images"),
        EnableDirectoryBrowsing = false
    });
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Migrate DB on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<AppDbContext>();

        if (context.Database.IsSqlServer())
            context.Database.Migrate();

        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await AppDbContextSeed.SeedDefaultUserAsync(userManager, roleManager);
        await AppDbContextSeed.SeedSampleDataAsync(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogError(ex, "An error occurred while migrating or seeding the database.");

        throw;
    }
}

app.Run();

// Make the Program class public using a partial class declaration (integration tests)
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program { }