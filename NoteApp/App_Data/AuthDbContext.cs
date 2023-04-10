using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;


namespace NoteApp.App_Data
{

    // DbContext - интерпретатор между БД и кодом
    public class AuthDbContext : IdentityDbContext<User>
    {

        // Настройка EF - задаём путь, создаём БД (если ещё не создана)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=.\App_Data\NoteUser.sqlite3");
            Database.EnsureCreated();
        }


        // Настройка для IdentityServer
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity => entity.ToTable(name: "Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));

            builder.ApplyConfiguration(new UserConfiguration());
        }


        // Таблицы, которые должны map'иться в БД
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
