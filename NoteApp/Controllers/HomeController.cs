using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;


namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        //[Authorize]
        public IActionResult Index()
        {
            var viewModel = new HomeViewModel();
            viewModel.Username = "test";

            ViewData["Info"] = viewModel;

            return View();
        }
    }
}
