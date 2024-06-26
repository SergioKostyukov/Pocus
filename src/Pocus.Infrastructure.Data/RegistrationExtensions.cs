﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Pocus.Infrastructure.Data;

public static class RegistrationExtensions
{
	public static void AddStorage(this IServiceCollection serviceCollection, IConfiguration configuration)
	{
        serviceCollection.AddDbContext<PocusDbContext>(options =>
		{
			options.UseSqlServer(configuration["ConnectionStrings:DefaultConnectionString"],
				options => options.MigrationsAssembly(typeof(PocusDbContext).Assembly.FullName));
		});
	}

	public static IServiceCollection AddBogusServices(this IServiceCollection services)
	{
		//services.AddTransient<UserDataGeneration>();

		return services;
	}
}
