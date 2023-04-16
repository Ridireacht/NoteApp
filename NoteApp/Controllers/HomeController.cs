using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;
using System;
using System.IO;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;


namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
			// Получаем ID пользователя, готовим ViewModel
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new HomeViewModel();

			viewModel.Notes = new List<Note>();


			// Достаём из БД список заметок и username
			using (var cxt = new AuthDbContext())
			{
                viewModel.Notes = (from c in cxt.Notes
								   where c.UserId == UserId
								   select c).ToList();

				viewModel.Username = cxt.Users.SingleOrDefault(b => b.Id == UserId).UserName;
            }


			ViewData["HomeModel"] = viewModel;
            return View();
        }


		[HttpGet]
		[Authorize]
		public IActionResult CreateNote()
		{
            int note_id;


			// Заносим в БД новую (пустую заметку)
			using (var cxt = new AuthDbContext())
			{
				Note nt = new Note()
				{
					UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
					Title = "",
                    Content = "",
					Image = System.IO.File.ReadAllBytes("wwwroot/images/default.jpg")
				};

				cxt.Notes.Add(nt);
				cxt.SaveChanges();


				// Запоминаем ID новой записи
				note_id = nt.Id;
			}


			// Переходим к новосозданной заметке
			return Redirect($"~/Home/Notes/{note_id}");
		}
	}
}
