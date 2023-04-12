using NoteApp.App_Data;


namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
			// ������ builder
			var host = CreateHostBuilder(args).Build();


			// ������ scope � ���������, ���������� �� ��
			using (var scope = host.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;
				try
				{
					var context = serviceProvider.GetRequiredService<AuthDbContext>();
					DbInitializer.Initialize(context);
				}

				catch (Exception exception)
				{
					var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
					logger.LogError(exception, "An error occurred while app initialization");
				}
			}


			host.Run();
		}

		
		// ���������� ��������� �� Startup.cs
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}