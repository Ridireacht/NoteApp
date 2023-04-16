using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;
using System.Security.Claims;


namespace NoteApp.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var viewModel = new AboutViewModel();


            using (var cxt = new AuthDbContext())
            {
                viewModel.Username = cxt.Users.SingleOrDefault(b => b.Id == UserId).UserName;
            }


            ViewData["AboutModel"] = viewModel;
            return View();
        }
    }
}
