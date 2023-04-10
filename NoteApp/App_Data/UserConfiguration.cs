using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoteApp.Models;

namespace NoteApp.App_Data
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(o => o.Notes).WithOne().HasForeignKey(d => d.UserID).IsRequired();
        }
    }
}
