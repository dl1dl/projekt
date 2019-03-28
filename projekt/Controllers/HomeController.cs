using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(IRecipeRepository rec, ICatRepository cat, IDiffLevelRepository dif)
        {
            _recipeRepository = rec;
            _categoryRepository = cat;
            _diffLevelRepository = dif;
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
    }
}