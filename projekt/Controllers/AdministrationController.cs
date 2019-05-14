using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using projekt.Models;
using projekt.Data;
using Microsoft.EntityFrameworkCore;

namespace projekt.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private UserManager<WebAppUser> _userManager;
        private IUserValidator<WebAppUser> _userValidator;
        private IPasswordValidator<WebAppUser> _passwordValidator;
        private IPasswordHasher<WebAppUser> _passwordHasher;
        private readonly AppDbContext _context;

        public AdministrationController(UserManager<WebAppUser> umn, IUserValidator<WebAppUser> uvl, 
            IPasswordValidator<WebAppUser> pvl, IPasswordHasher<WebAppUser> psh, AppDbContext ctx)
        {
            _userManager = umn;
            _userValidator = uvl;
            _passwordValidator = pvl;
            _passwordHasher = psh;
            _context = ctx;
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<ViewResult> Users()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        public async Task<ViewResult> Recipes()
        {
            return View(await _context.Recipes
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Include(x => x.DifficultyLevel)
                .ToListAsync());
        }

        public async Task<ViewResult> Comments()
        {
            return View(await _context.Comments
                .Include(x => x.Author)
                .Include(x => x.Recipe)
                .ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            //WebAppUser user = await _userManager.FindByIdAsync(id);
            WebAppUser user = await _context.Users
                .Include(r => r.Recipes)
                    .ThenInclude(a => a.Author)
                .Include(c => c.Comments)
                    .ThenInclude(a => a.Author)
                .Include(f => f.FavoriteRecipes)
                .FirstOrDefaultAsync(x => x.Id == id);

            foreach (Recipe recipe in user.Recipes)
            {
                recipe.Author = null;
            }
            foreach (Comment comment in user.Comments)
            {
                comment.Author = null;
            }
            foreach (FavoriteRecipe favoriteRecipe in user.FavoriteRecipes)
            {
                _context.FavoriteRecipes.Remove(favoriteRecipe);
            }

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
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
            return View("Users");
        }

        public async Task<IActionResult> EditUser(string id)
        {
            WebAppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Users");
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
                        return RedirectToAction("Users");
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


        [HttpPost]
        public IActionResult DeleteRecipe(int id)
        {
            Recipe recipe = _context.Recipes
                .Include(r => r.Comments)
                .FirstOrDefault(p => p.RecipeID == id);

            foreach (Comment comment in recipe.Comments)
            {
                _context.Comments.Remove(comment);
            }

            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }

            return RedirectToAction("Recipes");
        }
    }
}