namespace ArticlesManager.Extensions.Services;

using ArticlesManager.Databases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HeimGuard;
using ArticlesManager.Services;
using ArticlesManager.Resources;
using Microsoft.EntityFrameworkCore;

public static class ServiceRegistration
{
    public static void AddInfrastructure(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
    {
        // DbContext -- Do Not Delete
        if (env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddDbContext<ArticlesDbContext>(options =>
                options.UseInMemoryDatabase($"Articles"));
        }
        else
        {
            var connectionString = config.GetValue<string>("DB_CONNECTION_STRING");
            if(string.IsNullOrEmpty(connectionString))
            {
                // this makes local migrations easier to manage. feel free to refactor if desired.
                connectionString = env.IsDevelopment() 
                    ? "Host=localhost;Port=52006;Database=dev_articlesmanager;Username=postgres;Password=postgres"
                    : throw new Exception("DB_CONNECTION_STRING environment variable is not set.");
            }

            services.AddDbContext<ArticlesDbContext>(options =>
                options.UseNpgsql(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ArticlesDbContext).Assembly.FullName))
                            .UseSnakeCaseNamingConvention());
        }

        // Auth -- Do Not Delete
        if (!env.IsEnvironment(Consts.Testing.FunctionalTestingEnvName))
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = config.GetValue<string>("AUTH_AUTHORITY");
                    options.Audience = config.GetValue<string>("AUTH_AUDIENCE");
                    options.RequireHttpsMetadata = false;// !env.IsDevelopment();
                });
        }

        services.AddAuthorization(options =>
        {
        });

        services.AddHeimGuard<UserPolicyHandler>()
            .MapAuthorizationPolicies()
            .AutomaticallyCheckPermissions();
    }
}
