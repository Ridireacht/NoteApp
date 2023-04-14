using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NoteApp.Controllers
{

	[Route("Home/[controller]")]
	public class NotesController : Controller
	{

		[HttpGet("{noteId}")]
		[Authorize]
		public IActionResult GetNote(int noteId)
		{
			// Проверяем, есть ли у этого пользователя заметка с таким ID.
			// Если её нет вовсе или она принадлежит другому, то кидаем 404.
			if ()
			{
				return View();
			}

			return View();
		}


	}
}
