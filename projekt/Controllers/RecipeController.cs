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
        private IRecipeRepository _recipeRepository;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;

        public RecipeController(IRecipeRepository rec, UserManager<WebAppUser> umn, SignInManager<WebAppUser> sim)
        {
            _recipeRepository = rec;
            _userManager = umn;
            _signInManager = sim;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult AddRecipe()
        {
            return View();
        }

        [HttpPost]
        public RedirectToActionResult AddRecipe(Recipe newRecipe)
        {
            if (ModelState.IsValid)
            {
                _recipeRepository.AddRecipe(newRecipe);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("AddRecipe", newRecipe);
        }
    }
}