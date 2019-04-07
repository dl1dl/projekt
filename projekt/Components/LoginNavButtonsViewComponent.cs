using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;

namespace projekt.Components
{
    public class LoginNavButtonsViewComponent : ViewComponent
    {
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;

        public LoginNavButtonsViewComponent(SignInManager<WebAppUser> sim, UserManager<WebAppUser> umn)
        {
            _signInManager = sim;
            _userManager = umn;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    return View("AdminLogged");
                }
                return View("Logged", user);

            }
            else
            {
                return View();
            }
        }
    }
}
