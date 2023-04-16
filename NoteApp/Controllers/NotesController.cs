using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;
using System.Security.Claims;


namespace NoteApp.Controllers
{

	[Route("Home/Notes")]
	public class NotesController : Controller
	{

		[HttpGet("{note_id}")]
		[Authorize]
		public IActionResult GetNote(int note_id)
		{
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var viewModel = new NoteViewModel();


            // Получаем из БД ту самую записку
			using (var cxt = new AuthDbContext())
			{
				var nt = cxt.Notes.Single(x => x.Id == note_id);

				viewModel.Id = note_id;
                viewModel.Title = nt.Title;
                viewModel.Content = nt.Content;
				viewModel.Username = cxt.Users.SingleOrDefault(b => b.Id == UserId).UserName;
            }


            ViewData["NoteModel"] = viewModel;
			return View("~/Views/Home/Notes/Note.cshtml", viewModel);
		}


		[HttpPost]
		[Authorize]
		public IActionResult UpdateNote(NoteViewModel viewModel)
		{
			// Проводим обновление данных
			using (var cxt = new AuthDbContext())
			{
				var nt = cxt.Notes.Find(viewModel.Id);
				if (nt != null)
				{
					nt.Title = viewModel.Title;
					nt.Content = viewModel.Content;

                    using (var memoryStream = new MemoryStream())
                    {
						if (viewModel.ImageUpload != null)
						{
							viewModel.ImageUpload.CopyTo(memoryStream);
							nt.Image = memoryStream.ToArray();
						}
                    }

					cxt.SaveChanges();
				}
			}

			return Redirect($"~/Home/Notes/{viewModel.Id}");
		}


/*		[HttpPost]
		[Authorize]
		public IActionResult DeleteNote(NoteViewModel viewModel)
		{
			// Удаляем заметку по её ID
			using (var cxt = new AuthDbContext())
			{
				Note nt = new Note() { Id = viewModel.Id };

				cxt.Notes.Remove(nt);
				cxt.SaveChanges();
			}

			return Redirect("~/Home");
		}*/

	}
}
