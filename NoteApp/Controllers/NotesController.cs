using Microsoft.AspNetCore.Mvc;


namespace NoteApp.Controllers
{

	[Route("Home/[controller]")]
	public class NotesController : Controller
	{

		[HttpGet ("{noteId}")]
		public IActionResult GetNote(int noteId)
		{
			return View();
		}
	}
}
