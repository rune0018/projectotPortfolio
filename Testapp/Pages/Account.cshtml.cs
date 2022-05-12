using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Testapp.models;

namespace Testapp.Pages
{
    [AllowAnonymous]
    public class AccountModel : PageModel
    {
        public void OnGet()
        {
            
        }

        public IActionResult onPostLogin(LoginModel model)
        {
            return Unauthorized();
            
        }
    }
}