using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;


namespace NoteApp.App_Data
{

    // DbContext - интерпретатор между БД и кодом
    public class AuthDbContext : IdentityDbContext<AppUser>
    {

        // Настройка EF - задаём путь
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=.\\App_Data\\NoteUser.sqlite3");
        }


        // Настройка для IdentityServer
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>(entity => entity.ToTable(name: "Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable(name: "Roles"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable(name: "UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable(name: "UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable(name: "UserLogins"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable(name: "UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable(name: "RoleClaims"));

            builder.ApplyConfiguration(new AppUserConfiguration());
        }


        // Таблицы, которые должны map'иться в БД (кроме Users, она делает
        // это по умолчанию)
        public DbSet<Note> Notes { get; set; }
    }
}
