namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ��������� ������� � ���������
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Models.AuthDbContext>();
            builder.Services.AddIdentityServer()
                .AddInMemoryApiResources(Configuration.ApiResources)
                .AddInMemoryIdentityResources(Configuration.IdentityResources)
                .AddInMemoryApiScopes(Configuration.ApiScopes)
                .AddInMemoryClients(Configuration.Clients)
                .AddDeveloperSigningCredential();


            // ����������� ����������
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