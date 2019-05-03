using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projekt.Models;
using projekt.Models.ViewModels;
using projekt.Data;

namespace projekt.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly AppDbContext _context;
        private IRecipeRepository _recipeRepository;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;

        public RecipeController(AppDbContext ctx, IRecipeRepository rec, UserManager<WebAppUser> umn, SignInManager<WebAppUser> sim)
        {
            _context = ctx;
            _recipeRepository = rec;
            _userManager = umn;
            _signInManager = sim;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<ViewResult> AddRecipe()
        {
            WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.AuthorID = user.Id;

            return View();
        }

        /*[HttpPost]
        public RedirectToActionResult AddRecipe(Recipe newRecipe)
        {
            if (ModelState.IsValid)
            {
                _recipeRepository.AddRecipe(newRecipe);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AddRecipe", newRecipe);
        }*/

        [HttpPost]
        public async Task<IActionResult> AddRecipe(Recipe newRecipe)
        {
            WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.AuthorID = user.Id;

            if (ModelState.IsValid)
            {
                _context.Recipes.Add(newRecipe);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            //user.Recipes.Add(newRecipe);

            return View(newRecipe);
        }
    }
}