using Microsoft.AspNetCore.Identity;
using NoteApp.Models;

namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Добавляем сервисы в контейнер
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Models.AuthDbContext>();

            builder.Services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();


            // Настраиваем приложение
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}