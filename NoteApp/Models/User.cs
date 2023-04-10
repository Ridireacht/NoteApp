using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace NoteApp.Models
{
    public class User : IdentityUser
    {
        [Key]
        public int ID { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
