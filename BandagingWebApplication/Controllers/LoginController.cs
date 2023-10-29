using BandagingWebApplication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace BandagingWebApplication.Controllers
{
    public class LoginController : Controller
    {
        private Service _service;

        public LoginController()
        {
            _service = new Service();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            var data = _service.Login(userName, password);

            if (data != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userName)
                };

                var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle, props).Wait();
                return RedirectToAction("Index", "Admin");
            }
            return View();
            //return new Result { IsSuccess = true, Data = data };

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




    }
}
