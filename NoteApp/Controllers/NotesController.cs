using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;
using System.Security.Claims;


namespace NoteApp.Controllers
{

	[Route("Home/[controller]")]
	public class NotesController : Controller
	{

		[HttpGet("{note_id}")]
		[Authorize]
		public IActionResult GetNote(int note_id)
		{
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var viewModel = new NoteViewModel();


            // Получаем из БД ту самую записку
            Note nt;
			using (var cxt = new AuthDbContext())
			{
				nt = cxt.Notes.Find(note_id);

				viewModel.Id = note_id;
				viewModel.Title = nt.Title;
				viewModel.Content = nt.Content;
				viewModel.Image = nt.Image;
				viewModel.Username = cxt.Users.SingleOrDefault(b => b.Id == UserId).UserName;
            }


            ViewData["NoteModel"] = viewModel;
			return View("~/Views/Home/Notes/Note.cshtml", viewModel);
		}


		[HttpPost("{note_id}")]
		[Authorize]
		public IActionResult UpdateNote(int note_id, NoteViewModel viewModel)
		{
			// Проводим обновление данных
			using (var cxt = new AuthDbContext())
			{
				var result = cxt.Notes.SingleOrDefault(b => b.Id == note_id);
				if (result != null)
				{
					result.Title = viewModel.Title;
					result.Content = viewModel.Content;
					cxt.SaveChanges();
				}
			}

			return View("~/Views/Home/Notes/Note.cshtml");
		}


		[HttpPost("{note_id}")]
		[Authorize]
		public IActionResult DeleteNote(int note_id)
		{
			// Удаляем заметку по её ID
			using (var cxt = new AuthDbContext())
			{
				Note nt = new Note() { Id = note_id };

				cxt.Notes.Remove(nt);
				cxt.SaveChanges();
			}

			return Redirect("~/Home");
		}
	}
}
