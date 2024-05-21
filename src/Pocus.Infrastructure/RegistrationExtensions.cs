using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pocus.Infrastructure.Interfaces;
using Pocus.Infrastructure.Services;
using Pocus.Infrastructure.Settings;

namespace Pocus.Infrastructure;

public static class RegistrationExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSenderSettings>(options => configuration.GetSection("EmailSettings").Bind(options));
        services.AddScoped<IEmailSender, EmailSender>();

        return services;
    }
}
