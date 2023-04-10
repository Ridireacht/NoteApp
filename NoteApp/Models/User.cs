using Microsoft.AspNetCore.Identity;


namespace NoteApp.Models
{
    public class User : IdentityUser
    {
        public string Password { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
