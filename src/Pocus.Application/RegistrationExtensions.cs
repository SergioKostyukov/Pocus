using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pocus.Application.Interfaces;
using Pocus.Application.MappingProfiles;
using Pocus.Application.Services;

namespace Pocus.Application;

public static class RegistrationExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(PlanProfile));
        services.AddAutoMapper(typeof(NoteProfile));
        services.AddAutoMapper(typeof(SettingsProfile));

        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<ISettingsService, SettingsService>();

        return services;
    }
}
