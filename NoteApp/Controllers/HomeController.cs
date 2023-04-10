using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
