using Pocus.Identity;
using Pocus.Infrastructure.Data;

namespace Pocus;
public class Program
{
	public static void Main(string[] args)
	{
		#region Configure services
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddStorage(builder.Configuration);

        builder.Services.AddJWTTokenServices(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

		builder.Services.AddBogusServices();

		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();
		#endregion

		#region Configure pipeline
		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=MainPage}");

		app.MapRazorPages();

		app.DatabaseEnsureCreated();

		app.Run();
		#endregion
	}
}
