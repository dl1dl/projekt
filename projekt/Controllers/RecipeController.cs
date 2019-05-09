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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace projekt.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;

        public RecipeController(AppDbContext ctx, IRecipeRepository rec, UserManager<WebAppUser> umn, SignInManager<WebAppUser> sim)
        {
            _context = ctx;
            _userManager = umn;
            _signInManager = sim;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult AddRecipe()
        {
            var categories = from c in _context.Categories orderby c.Name select c;
            ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", null);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(NewRecipeVM newRecipe)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = new Recipe()
                {
                    Author = await _userManager.GetUserAsync(HttpContext.User),
                    Name = newRecipe.Name,
                    Body = newRecipe.Body,
                    Category = await _context.Categories.Where(x => x.CategoryID == newRecipe.Category).SingleAsync()
                };
                
                _context.Recipes.Add(recipe);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            var categories = from c in _context.Categories orderby c.Name select c;
            ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", newRecipe.Category);

            return View(newRecipe);
        }

        [AllowAnonymous]
        public async Task<ViewResult> Details(int id)
        {
            Recipe recipe = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .Include(r => r.DifficultyLevel)
                .Include(r => r.Comments).ThenInclude(c => c.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RecipeID == id);
            ViewBag.recipeName = recipe.Name;

            return View(recipe);
        }
    }
}