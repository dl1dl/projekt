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
        private IUserValidator<WebAppUser> _userValidator;
        private IPasswordValidator<WebAppUser> _passwordValidator;
        private IPasswordHasher<WebAppUser> _passwordHasher;

        public AdministrationController(UserManager<WebAppUser> umn, IUserValidator<WebAppUser> uvl, 
            IPasswordValidator<WebAppUser> pvl, IPasswordHasher<WebAppUser> psh)
        {
            _userManager = umn;
            _userValidator = uvl;
            _passwordValidator = pvl;
            _passwordHasher = psh;
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

        public async Task<IActionResult> EditUser(string id)
        {
            WebAppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("UsersList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string email, string password)
        {
            WebAppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await _userValidator.ValidateAsync(_userManager, user);
                if (!validEmail.Succeeded)
                {
                    foreach (IdentityError error in validEmail.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    user.UserName = user.Email;
                }

                IdentityResult validPassword = null;
                bool emptyPassword = string.IsNullOrEmpty(password);
                if (!emptyPassword)
                    {
                    validPassword = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (!validPassword.Succeeded)
                    {
                        foreach (IdentityError error in validPassword.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                    else
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    }
                }
                if ((validEmail.Succeeded && emptyPassword) ||
                    (validEmail.Succeeded && validPassword.Succeeded && !emptyPassword))
                    {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UsersList");
                    }
                    else
                    {
                        foreach (IdentityError error in validPassword.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return View(user);
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika");
            }
            return View(user);
        }
    }
}