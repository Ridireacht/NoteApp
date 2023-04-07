namespace NoteApp.Models
{
    public class DbInitializer
    {
        public static void Initialize(AuthDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
