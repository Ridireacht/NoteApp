using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;
using System.Security.Claims;

// Проверяем, есть ли у этого пользователя заметка с таким ID.
// Если её нет вовсе или она принадлежит другому, то кидаем 404.


namespace NoteApp.Controllers
{

	[Route("Home/[controller]")]
	public class NotesController : Controller
	{

		[HttpGet("{note_id}")]
		[Authorize]
		public IActionResult GetNote(int note_id)
		{
			return View();
		}


		[HttpPost("{note_id}")]
		[Authorize]
		public IActionResult UpdateNote(int note_id)
		{
			return View();
		}


		[HttpPost("{note_id}")]
		[Authorize]
		public IActionResult DeleteNote(int note_id)
		{
			// Удаляем заметку по её ID
			using (var cxt = new AuthDbContext(default))
			{
				Note nt = new Note() { Id = note_id };

				cxt.Notes.Remove(nt);
				cxt.SaveChanges();
			}

			return View();
		}
	}
}
