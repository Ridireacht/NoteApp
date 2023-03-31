using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using System.Diagnostics;


namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}