namespace NoteApp.Models
{
	public class NoteViewModel
	{
		public int Id { get; set; }

		public string? Title { get; set; }
		public string? Content { get; set; }

		public string Username { get; set; }

		public IFormFile ImageUpload { get; set; }
    }
}
