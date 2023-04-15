﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;
using System.Security.Claims;


namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new HomeViewModel();

			//viewModel.Username = User.FindFirst(ClaimTypes.Name).Value;
			viewModel.Notes = new List<Note>();

			using (var cxt = new AuthDbContext())
			{
                viewModel.Notes = (from c in cxt.Notes
								   where c.UserId == UserId
								   select c).ToList();
            }

			ViewData["HomeModel"] = viewModel;
            return View();
        }


		[HttpPost]
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
					CreationDate = DateTime.Now,
					LastModified = DateTime.Now
				};

				cxt.Notes.Add(nt);
				cxt.SaveChanges();


				// Запоминаем ID новой записи
				note_id = nt.Id;
			}


			// Переходим к новосозданной заметке
			return Redirect($"~Home/Notes/{note_id}");
		}
	}
}
