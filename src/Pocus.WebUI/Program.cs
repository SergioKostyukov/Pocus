using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pocus.Core.Entities;
using Pocus.Infrastructure.Data;

namespace Pocus;

public class Program
{
    public static void Main(string[] args)
    {
        #region Configure services
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("PocusDbContextConnection") 
            ?? throw new InvalidOperationException("Connection string 'PocusDbContextConnection' not found.");

        builder.Services.AddStorage(builder.Configuration);

        builder.Services.AddDefaultIdentity<User>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
        })
            .AddEntityFrameworkStores<PocusDbContext>()
            .AddDefaultTokenProviders();

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
                pattern: "{controller=Home}/{action=Index}");

        app.MapRazorPages();

        app.DatabaseEnsureCreated();

        app.Run();
        #endregion
    }
}
