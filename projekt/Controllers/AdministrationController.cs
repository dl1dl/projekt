using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using Microsoft.AspNetCore.Identity;

namespace projekt.Controllers
{
    public class AdministrationController : Controller
    {
        private UserManager<WebAppUser> _userManager;

        public AdministrationController(UserManager<WebAppUser> umn)
        {
            _userManager = umn;
        } 

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult UsersList()
        {
            return View(_userManager.Users);
        }

        public ViewResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel newuser)
        {
            if (ModelState.IsValid)
            {
                WebAppUser user = new WebAppUser
                {
                    UserName = newuser.Email,
                    Email = newuser.Email
                };

                IdentityResult result = await _userManager.CreateAsync(user, newuser.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("UsersList");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(newuser);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            WebAppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("UsersList");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Brak takiego użytkownika");
            }
            return View("UsersList");
        }
    }
}