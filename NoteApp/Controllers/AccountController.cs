using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            var viewModel = new SignInViewModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel viewModel)
        {
            return View(viewModel);
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
