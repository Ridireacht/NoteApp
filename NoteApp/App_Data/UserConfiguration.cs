using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Models;


namespace NoteApp.App_Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Настраиваем отношение между таблицами.
            // У каждого User может быть много Note, связь между ними: User.ID + Note.UserID
            builder.HasMany(o => o.Notes).WithOne().HasForeignKey(d => d.Username).IsRequired();
        }
    }
}
