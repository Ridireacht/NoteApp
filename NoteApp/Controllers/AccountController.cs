﻿using IdentityServer4.Services;
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
        public IActionResult Login()
        {
			return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            // Если введённые данные не валидны, то возвращаем ту же форму.
            // Попутно cобираем все ошибки (это для просмотра при дебаге)
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

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
        public IActionResult Register()
        {
			return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            // Если введённые данные не валидны, то возвращаем ту же форму.
            // Попутно cобираем все ошибки (это для просмотра при дебаге)
            if (!ModelState.IsValid)
            {
                string messages = string.Join("; ", ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage));

                return View(viewModel);
            }


            // Иначе пытаемся занести пользователя в БД
            var user = new AppUser { UserName = viewModel.Username };
            var result = await _userManager.CreateAsync(user, viewModel.Password);


            // Если вышло, то перенаправляем пользователя на главную
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
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Redirect("~/Account/Login");
        }

    }
}
