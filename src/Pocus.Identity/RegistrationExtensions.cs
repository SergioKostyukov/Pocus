using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Pocus.Identity;

public static class RegistrationExtensions
{
    public static IServiceCollection AddJWTTokenServices(this IServiceCollection services, IConfiguration configuration)
    {
        var bindJwtSettings = new JwtSettings();
        configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

        services.AddSingleton(bindJwtSettings);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                ValidateIssuer = bindJwtSettings.ValidateIssuer,
                ValidIssuer = bindJwtSettings.ValidIssuer,
                ValidateAudience = bindJwtSettings.ValidateAudience,
                ValidAudience = bindJwtSettings.ValidAudience,
                RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                ClockSkew = TimeSpan.Zero,
            };
        });

        return services;
    }
}
