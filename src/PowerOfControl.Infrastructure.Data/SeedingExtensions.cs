using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Pocus.Infrastructure.Data;

public static class SeedingExtensions
{
	public static async Task DatabaseEnsureCreated(this IApplicationBuilder applicationBuilder)
	{
		using var scope = applicationBuilder.ApplicationServices.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<PocusDbContext>();

	}
}
