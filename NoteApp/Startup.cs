using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;	
using System.Security.Claims;
using NoteApp.App_Data;
using NoteApp.Models;

namespace NoteApp
{
	// Класс для настройки проекта
	public class Startup
	{

		// Задаём конфигурацию
		public IConfiguration AppConfiguration { get; }
		public Startup(IConfiguration configuration) =>
			AppConfiguration = configuration;


		// Добавляем и конфигурируем сервисы
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = AppConfiguration.GetValue<string>("DbConnection");

			services.AddDbContext<AuthDbContext>(options =>
			{
				options.UseSqlite(connectionString);
			});

			services.AddIdentity<User, IdentityRole>(config =>
			{
				config.Password.RequiredLength = 4;
				config.Password.RequireDigit = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireUppercase = false;
			})
				.AddEntityFrameworkStores<AuthDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer()
				.AddAspNetIdentity<User>()
				.AddInMemoryApiResources(Configuration.ApiResources)
				.AddInMemoryIdentityResources(Configuration.IdentityResources)
				.AddInMemoryApiScopes(Configuration.ApiScopes)
				.AddInMemoryClients(Configuration.Clients)
				.AddDeveloperSigningCredential();

			services.ConfigureApplicationCookie(config =>
			{
				config.Cookie.Name = "NoteApp.Identity.Cookie";
				config.LoginPath = "/Account/SignIn";
				config.LogoutPath = "/Account/SignOut";
			});

			services.Configure<IdentityOptions>(options => options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
			services.AddControllersWithViews();
		}


		// Конфигурируем само приложение
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}


			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine(env.ContentRootPath, "Styles")),
				RequestPath = "/styles"
			});


			app.UseRouting();
			app.UseAuthentication();
			app.UseIdentityServer();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapDefaultControllerRoute();
			});
		}
	}
}
