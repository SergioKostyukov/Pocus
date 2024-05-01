using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pocus.Identity.MappingProfiles;

namespace Pocus.Identity;

public static class RegistrationExtensions
{
    public static IServiceCollection AddJWTTokenServices(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JsonWebTokenKeys").Get<JwtSettings>();
        services.Configure<JwtSettings>(configuration.GetSection("JsonWebTokenKeys"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSettings.IssuerSigningKey)),
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidIssuer = jwtSettings.ValidIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidAudience = jwtSettings.ValidAudience,
                RequireExpirationTime = jwtSettings.RequireExpirationTime,
                ValidateLifetime = jwtSettings.RequireExpirationTime,
                ClockSkew = TimeSpan.Zero,
            };
        })
        .AddCookie("Cookie", options =>
        {
            options.LoginPath = "~Views/Identity/Login.cshtml";
        });

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
