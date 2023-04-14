namespace NoteApp.Models
{
	public class NoteViewModel
	{
		public int Id { get; set; }

		public string? Title { get; set; }
		public string? Content { get; set; }

		public DateTime CreationDate { get; set; }
		public DateTime LastModified { get; set; }
	}
}
