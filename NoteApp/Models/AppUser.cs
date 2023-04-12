using Microsoft.AspNetCore.Identity;


namespace NoteApp.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Note> Notes { get; set; }
    }
}
