using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projekt.Data;

namespace projekt.Controllers
{
    public class HomeController : Controller
    {
        private IRecipeRepository _recipeRepository;
        private ICatRepository _categoryRepository;
        private IRecipeRepository _diffLevelRepository;

        public HomeController(IRecipeRepository rec, ICatRepository cat, IRecipeRepository dif)
        {
            _recipeRepository = rec;
            _categoryRepository = cat;
            _diffLevelRepository = dif;
        }

        public IActionResult Index()
        {
            return View(_recipeRepository.Recipes);
        }
    }
}