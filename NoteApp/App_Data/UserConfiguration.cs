using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Models;


namespace NoteApp.App_Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Указываем, что Id - это ключ
            builder.HasKey(x => x.Id);

            // Настраиваем отношение между таблицами.
            // У каждого User может быть много Note, связь между ними: User.Id + Note.UserId
            builder.HasMany(o => o.Notes).WithOne().HasForeignKey(d => d.UserId).IsRequired();
        }
    }
}
