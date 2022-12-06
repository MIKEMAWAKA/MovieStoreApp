using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStoreApp.Models.DTO;
using MovieStoreApp.Repository.Abstract;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieStoreApp.Controllers
{
    public class UserAuthenticationController : Controller
    {
        private readonly IUserAuthenticationService authService;

        public UserAuthenticationController(IUserAuthenticationService authenticationService)
        {
            this.authService = authenticationService;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }


         //We will create a user with admin rights, after that we are going
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] Registration registration)
        {
            var model = new Registration
            {
                Email = registration.Email,
                Username = registration.Username,
                Name = registration.Name,
                Password = registration.Password,
                PasswordConfirm = registration.PasswordConfirm,
                Role = registration.Role
            };
            // if you want to register with user , Change Role="User"
            var result = await authService.RegisterAsync(model);
            return Ok(result.Message);
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await authService.LoginAsync(model);
            if (result.StatusCode == 1)
                return RedirectToAction("Index", "Home");
            else
            {
                TempData["msg"] = "Could not logged in..";
                return RedirectToAction(nameof(Login));
            }
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}

