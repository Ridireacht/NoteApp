using IdentityServer4.Models;
using IdentityServer4;


namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Add services to the container
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Models.Context>();
            builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();

            // App setup
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityServer();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}");

            app.Run();
        }
    }
}