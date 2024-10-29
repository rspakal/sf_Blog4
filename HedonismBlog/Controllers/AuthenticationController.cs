using HedonismBlog.DataAccess.Repositories;
using HedonismBlog.Models;
using HedonismBlog.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace HedonismBlog.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        public AuthenticationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("SignIn")]
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        [Route("SubmitSignIn")]
        public async Task<IActionResult> SubmitSignin(UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SignIn");
            }
            var user = await _userRepository.GetByEmail(viewModel.Email);
            if (user == null)
            {
                ViewBag.Message = $"No user with '{viewModel.Email}' email is registered";
                return View("SignIn");
            }

            if (user.Password != viewModel.Password)
            {
                ViewBag.Message = $"Wrong password for '{viewModel.Email}'";
                return View("SignIn");
            }
          
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.Email, user.Email)

            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
