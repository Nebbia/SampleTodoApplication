using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Todo.Services;
using Todo.Web.ViewModels;

namespace Todo.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthenticateUserService _authenticateUserService;

        public LoginController(AuthenticateUserService authenticateUserService)
        {
            _authenticateUserService = authenticateUserService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginRequest)
        {
            if (!ModelState.IsValid)
            {
                loginRequest.Password = string.Empty;
                return View(loginRequest);
            }

            var authedUser = await _authenticateUserService.GetUserByUsernameAndPassword(
                loginRequest.Username,
                loginRequest.Password);

            return RedirectToAction("Index", "List");
        }
    }
}