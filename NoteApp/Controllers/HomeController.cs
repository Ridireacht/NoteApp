using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var viewModel = new HomeViewModel();
            viewModel.Username = User.FindFirst(ClaimTypes.NameIdentifier).Value;

			ViewData["Info"] = viewModel;

            return View();
        }
    }
}
