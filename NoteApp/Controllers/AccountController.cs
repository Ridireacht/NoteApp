using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;


namespace NoteApp.Controllers
{

    // AccountController управляет авторизацией и регистрацией
    public class AccountController : Controller
    {

        // Штуки для работы IdentityServer. Первая - для входа, вторая -
        // для работы с пользователем, третья - для logout'а
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IIdentityServerInteractionService _interactionService;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IIdentityServerInteractionService interactionService)
            => (_signInManager, _userManager, _interactionService) = (signInManager, userManager, interactionService);


        [HttpGet]
        public IActionResult SignIn()
        {
            var viewModel = new SignInViewModel();
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel viewModel)
        {
            // Если введённые данные не валидны, то возвращаем ту же форму.
            // Попутно выводим в консоль все ошибки (это для дебага)
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                foreach (var error in allErrors) { Console.WriteLine(error); }

                return View(viewModel);
            }


            // Иначе пытаемся найти пользователя в БД. Если не находим, выдаём ошибку
            // и возвращаем ту же форму авторизации
            var user = await _userManager.FindByNameAsync(viewModel.Username);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "User not found");
                return View(viewModel);
            }


            // Если пользователь есть, то пытаемся залогиниться
            var result = await _signInManager.PasswordSignInAsync(viewModel.Username, viewModel.Password, false, false);

            
            // Если вышло, то перенаправляем пользователя на главную
            if (result.Succeeded)
                return Redirect("~/Home");


            // Иначе выкидываем ошибку и возвращаем ту же форму авторизации
            ModelState.AddModelError(string.Empty, "Login error");
            return View(viewModel);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            var viewModel = new SignUpViewModel();
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
        {
            // Если введённые данные не валидны, то возвращаем ту же форму.
            // Попутно выводим в консоль все ошибки (это для дебага)
            if (!ModelState.IsValid)
            {
                var allErrors = ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage));

                foreach (var error in allErrors) { Console.WriteLine(error); }

                return View(viewModel);
            }


            // Иначе пытаемся занести пользователя в БД. Если вышло, то перенаправляем
            // пользователя на главнуб.
            var user = new AppUser { UserName = viewModel.Username };

            var result = await _userManager.CreateAsync(user, viewModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return Redirect("~/Home");
            }


            // Если не вышло, возвращаем ошибку и ту же форму регистрации
            ModelState.AddModelError(string.Empty, "Error occured");
            return View(viewModel);
        }


        [HttpGet]
        public async Task<IActionResult> SignOut(string logoutId)
        {
            await _signInManager.SignOutAsync();

            var logoutRequest = await _interactionService.GetLogoutContextAsync(logoutId);

            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

    }
}
