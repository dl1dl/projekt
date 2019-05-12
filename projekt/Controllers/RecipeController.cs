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

        public ViewResult Add()
        {
            var categories = from c in _context.Categories orderby c.Name select c;
            ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", null);

            var dLevels = from d in _context.DifficultyLevels orderby d.Name select d;
            ViewBag.DifficultyLevels = new SelectList(dLevels.AsNoTracking(), "DifficultyLevelID", "Name", null);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewRecipeVM newRecipe)
        {
            if (ModelState.IsValid)
            {
                Recipe recipe = new Recipe()
                {
                    Author = await _userManager.GetUserAsync(HttpContext.User),
                    Name = newRecipe.Name,
                    Body = newRecipe.Body,
                    Category = await _context.Categories.Where(x => x.CategoryID == newRecipe.Category).SingleAsync(),
                    DifficultyLevel = await _context.DifficultyLevels.Where(x => x.DifficultyLevelID == newRecipe.DifficultyLevel).SingleAsync()
                };
                
                _context.Recipes.Add(recipe);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            var categories = from c in _context.Categories orderby c.Name select c;
            ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", newRecipe.Category);

            var dLevels = from d in _context.DifficultyLevels orderby d.Name select d;
            ViewBag.DifficultyLevels = new SelectList(dLevels.AsNoTracking(), "DifficultyLevelID", "Name", newRecipe.DifficultyLevel);

            return View(newRecipe);
        }

        public IActionResult Edit(int id)
        {
            Recipe recipe = _context.Recipes
                .Include(x => x.Category)
                .Include(x => x.DifficultyLevel)
                .FirstOrDefault(p => p.RecipeID == id);

            if (recipe != null)
            {
                EditRecipeVM recipeToEdit = new EditRecipeVM()
                {
                    Name = recipe.Name,
                    Body = recipe.Body,
                    Category = recipe.Category.CategoryID,
                    DifficultyLevel = recipe.DifficultyLevel.DifficultyLevelID,
                    OriginalRecipe = id
                };

                var categories = from c in _context.Categories orderby c.Name select c;
                ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", recipeToEdit.Category);

                var dLevels = from d in _context.DifficultyLevels orderby d.Name select d;
                ViewBag.DifficultyLevels = new SelectList(dLevels.AsNoTracking(), "DifficultyLevelID", "Name", recipeToEdit.DifficultyLevel);

                return View(recipeToEdit);
            }
            return RedirectToAction("Recipes");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRecipeVM recipe)
        {
            if (ModelState.IsValid)
            {
                Recipe originalRecipe = await _context.Recipes.FirstOrDefaultAsync(p => p.RecipeID == recipe.OriginalRecipe);
                if (originalRecipe != null)
                {
                    originalRecipe.Name = recipe.Name;
                    originalRecipe.Body = recipe.Body;
                    originalRecipe.Category = await _context.Categories.Where(x => x.CategoryID == recipe.Category).SingleAsync();
                    originalRecipe.DifficultyLevel = await _context.DifficultyLevels.Where(x => x.DifficultyLevelID == recipe.DifficultyLevel).SingleAsync();
                    _context.SaveChanges();
                    return RedirectToAction("Recipes", "Administration");
                }
            }

            var categories = from c in _context.Categories orderby c.Name select c;
            ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", recipe.Category);

            var dLevels = from d in _context.DifficultyLevels orderby d.Name select d;
            ViewBag.DifficultyLevels = new SelectList(dLevels.AsNoTracking(), "DifficultyLevelID", "Name", recipe.DifficultyLevel);

            return View(recipe);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            Recipe recipe = await _context.Recipes
                .Include(r => r.Author)
                .Include(r => r.Category)
                .Include(r => r.DifficultyLevel)
                .Include(r => r.Comments).ThenInclude(c => c.Author)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RecipeID == id);

            ViewBag.isAuthor = false;
            ViewBag.isInFavorites = false;
            ViewBag.isLogged = false;

            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                ViewBag.isLogged = true;
                WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (recipe.Author.Id == user.Id)
                {
                    ViewBag.isAuthor = true;
                }
                else
                {
                    if (_context.FavoriteRecipes.Any())
                    {
                        FavoriteRecipe favoriteRecipe = await _context.FavoriteRecipes
                    .Include(r => r.Recipe)
                    .Include(r => r.User)
                    .Where(r => r.Recipe.RecipeID == id && r.User.Id == user.Id)
                    .SingleAsync();

                        if (!(favoriteRecipe == null))
                        {
                            ViewBag.isInFavorites = true;
                            ViewBag.favoriteRecipeID = favoriteRecipe.FavoriteRecipeID;
                        }
                    }
                }
            }

            return View(recipe);
        }

        public async Task<IActionResult> AddToFavorites(int id)
        {
            FavoriteRecipe favoriteRecipe = new FavoriteRecipe()
            {
                User = await _userManager.GetUserAsync(HttpContext.User),
                Recipe = await _context.Recipes.Where(x => x.RecipeID == id).SingleAsync()
            };

            _context.FavoriteRecipes.Add(favoriteRecipe);
            _context.SaveChanges();

            return RedirectToAction("Details", "Recipe", new { id = id });
        }

        public async Task<IActionResult> RemoveFromFavorites(int id)
        {
            FavoriteRecipe favoriteRecipe = await _context.FavoriteRecipes
                .Include(r => r.Recipe)
                .Where(r => r.FavoriteRecipeID == id).SingleAsync();

            int returnId = favoriteRecipe.Recipe.RecipeID;

            _context.FavoriteRecipes.Remove(favoriteRecipe);
            _context.SaveChanges();

            return RedirectToAction("Details", "Recipe", new { id = returnId });
        }
    }
}