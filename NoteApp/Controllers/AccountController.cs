using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;


namespace NoteApp.Controllers
{

    // AccountController управляет авторизацией и регистрацией
    public class AccountController : Controller
    {

        // Штуки для работы IdentityServer
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, IIdentityServerInteractionService interactionService)
            => (_signInManager, _userManager, _interactionService) = (signInManager, userManager, interactionService);


        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            var viewModel = new SignInViewModel { ReturnUrl = returnUrl };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = await _userManager.FindByNameAsync(viewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

            if (result.Succeeded)
            {
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Login error");

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SignUp(string returnUrl)
        {
            var viewModel = new SignUpViewModel { ReturnUrl = returnUrl };

            return View(returnUrl);
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var user = new User
            {
                UserName = viewModel.Username
            };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect(viewModel.ReturnUrl);
            }

            ModelState.AddModelError(string.Empty, "Error occured");

            return View(viewModel);
        }
    }
}
