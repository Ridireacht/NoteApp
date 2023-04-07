using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NoteApp.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<UserConfiguration>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

        }
    }
}
