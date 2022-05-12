using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Testapp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace Testapp.Pages.Account
{
    public class LoginModel : PageModel
    {
        private UserRepo users = new();

        [BindProperty]
        public string ReturnUrl { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public bool RememberLogin { get; set; }

        public void OnGet(string ReturnUrl = "/")
        {
            this.ReturnUrl = ReturnUrl;
        }

       
        public async Task<IActionResult> OnPostLoginAsync(models.LoginModel model)
        {
            //var model = new models.LoginModel { Password = password, Username = username, ReturnUrl = this.ReturnUrl, RememberLogin = rememberlogin };
            if(model.ReturnUrl == null)
            {
                model.ReturnUrl = "/";
            }
            var user = users.GetUser(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var identity = new ClaimsIdentity(claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = model.RememberLogin });
            Console.WriteLine(principal.Identities.GetHashCode());
            return LocalRedirect(model.ReturnUrl);
        }
        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
