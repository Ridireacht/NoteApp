namespace NoteApp.Models
{
    public class HomeViewModel
    {
        public string Username { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
