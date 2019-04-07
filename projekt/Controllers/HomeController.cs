using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using projekt.Data;
using projekt.Models;
using projekt.Models.ViewModels;

namespace projekt.Controllers
{
    public class HomeController : Controller
    {
        private IRecipeRepository _recipeRepository;
        private ICatRepository _categoryRepository;
        private IDiffLevelRepository _diffLevelRepository;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;

        public HomeController(IRecipeRepository rec, ICatRepository cat, IDiffLevelRepository dif, 
            UserManager<WebAppUser> umn, SignInManager<WebAppUser> sim)
        {
            _recipeRepository = rec;
            _categoryRepository = cat;
            _diffLevelRepository = dif;
            _userManager = umn;
            _signInManager = sim;
        }

        public IActionResult Index()
        {
            return View(new PrintVM
            {
                Recipes = _recipeRepository.Recipes,
                Categories = _categoryRepository.Categories,
                DiffLevels = _diffLevelRepository.DifficultyLevels
            });
        }

        public ViewResult Details(int recipeID)
        {
            Recipe recipe = _recipeRepository.Recipes.FirstOrDefault(x => x.RecipeID == recipeID);
            ViewBag.recipeName = recipe.Name;

            return View(recipe);
        }

        /*[Authorize]
        public ViewResult AddRecipe() 
        {
            return View(new Recipe());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    recipe.Auth = user;
                    _recipeRepository.AddRecipe(recipe);
                    return RedirectToAction("Index");
                }
            }
            return View(recipe);
        }*/

    }
}