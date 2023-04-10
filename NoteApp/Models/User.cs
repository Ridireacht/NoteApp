using Microsoft.AspNetCore.Identity;


namespace NoteApp.Models
{
    public class User : IdentityUser
    {
        public ICollection<Note> Notes { get; set; }
    }
}
