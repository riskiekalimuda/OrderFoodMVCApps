using FoodOrderEntitie.Models;
using FoodOrderEntitie.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using FoodOrderEntitie.AdminViewModels;

namespace FoodOrderMVC.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewDetail login)
        {
            var obj = Administrator.AdminLogin(login);
            if (obj != null && obj.Result == 1)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, login.Username)
                };
                ClaimsIdentity claimsIdentitty = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = false
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentitty), properties);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrMessage = obj.ResultMessage;
            }
            return RedirectToAction("Index", "Login");
        }
    }
}
