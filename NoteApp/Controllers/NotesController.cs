using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;


namespace NoteApp.Controllers
{

	[Route("Home/[controller]")]
	public class NotesController : Controller
	{

		[HttpGet("{note_id}")]
		[Authorize]
		public IActionResult GetNote(int note_id)
		{
			// Получаем из БД ту самую записку
			Note nt;
			using (var cxt = new AuthDbContext(default))
			{
				nt = cxt.Notes.Find(note_id);
			}
			
			
			// Сохраняем инфу для вывода
			var viewModel = new NoteViewModel()
			{
				Id = note_id,
				Title = nt.Title,
				Content = nt.Content,
				CreationDate = nt.CreationDate,
				LastModified = nt.LastModified
			};
			ViewData["Note"] = viewModel;

			return View();
		}


		[HttpPost("{note_id}")]
		[Authorize]
		public IActionResult UpdateNote(int note_id, NoteViewModel viewModel)
		{
			// Проводим обновление данных
			using (var cxt = new AuthDbContext(default))
			{
				var result = cxt.Notes.SingleOrDefault(b => b.Id == note_id);
				if (result != null)
				{
					result.Title = viewModel.Title;
					result.Content = viewModel.Content;
					result.CreationDate = viewModel.CreationDate;
					result.LastModified = viewModel.LastModified;
					cxt.SaveChanges();
				}
			}

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

			return Redirect("~/Home");
		}
	}
}
