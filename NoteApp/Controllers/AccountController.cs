using Microsoft.AspNetCore.Mvc;


namespace NoteApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
