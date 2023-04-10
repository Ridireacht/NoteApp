using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteApp.App_Data;
using NoteApp.Models;
using System.Net;

namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Добавляем сервисы в контейнер
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlite(@"Data Source=C:\Projects\NoteApp\NoteApp\App_Data\NoteUser.sqlite3");
            });

            builder.Services.AddRazorPages();

            builder.Services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 8;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireDigit = true;
                config.Password.RequireUppercase = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredUniqueChars = 0;
            })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "Notes.Identity.Cookie";
                config.LoginPath = "/Account/SignIn";
                config.LogoutPath = "/Account/SignOut";
            });

            // Аутентификация в приложении, конфигурация 
            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:25798/";
                    options.Audience = "NoteApp";
                    options.RequireHttpsMetadata = false;
                });



            // Настраиваем приложение
            var app = builder.Build();

            // Если не авторизован и попал на сторонние страницы, то редирект
            // на страницу входа
            app.UseStatusCodePages(async context =>
            {
                var response = context.HttpContext.Response;

                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                        response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/Account/SignIn");
            });

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });


            // Проверяем, существует ли БД
            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<AuthDbContext>();
                    DbInitializer.Initialize(context);
                }

                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while DB creation!");
                }
            }


            app.Run();
        }
    }
}