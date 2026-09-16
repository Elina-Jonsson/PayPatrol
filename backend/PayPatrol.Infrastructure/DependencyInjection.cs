using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PayPatrol.Application.Interfaces;
using PayPatrol.Application.Services;
using PayPatrol.Infrastructure.Data;
using PayPatrol.Infrastructure.Identity;
using PayPatrol.Infrastructure.Repositories;
using System.Text;

namespace PayPatrol.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()));

            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>();
                //.AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            // Application Services
            services.AddScoped<IServiceCatalogService, ServiceCatalogService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Configure JWT settings and register the JWT token generator service
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            ArgumentNullException.ThrowIfNull(jwtSettings, "Jwt-section missing in appsettings.json");

            services.AddSingleton<JwtSettings>(jwtSettings);
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            // Configure authentication with JWT
            // Doorman
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Examine key when a request is made to validate the token
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };

                options.Events = new JwtBearerEvents
                {
                    // Bridge between cookie and JWT token. If a JWT token is stored in a cookie, extract it and use it for authentication.
                    OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.TryGetValue("jwt", out var token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}