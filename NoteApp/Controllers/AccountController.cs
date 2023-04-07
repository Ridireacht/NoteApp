﻿using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class AccountController : Controller
    {
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

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
