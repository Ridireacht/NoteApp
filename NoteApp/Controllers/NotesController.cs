using Microsoft.AspNetCore.Mvc;


namespace NoteApp.Controllers
{
	public class NotesController : Controller
	{
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
	}
}
