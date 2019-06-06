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
using System.Text.RegularExpressions;

namespace projekt.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;
        private RecipeDetailsVM recipeDetailsVM;

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

            NewRecipeVM newRecipe = new NewRecipeVM() { };

            return View(newRecipe);
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
                    Description = newRecipe.Description,
                    Category = await _context.Categories.Where(x => x.CategoryID == newRecipe.Category).SingleAsync(),
                    DifficultyLevel = await _context.DifficultyLevels
                        .Where(x => x.DifficultyLevelID == newRecipe.DifficultyLevel).SingleAsync(),
                    Tags = Regex.Replace(newRecipe.Tags, " {2,}", " ")
            };
 
                _context.Recipes.Add(recipe);
                _context.SaveChanges();

                foreach (var ingredient in newRecipe.Ingredients)
                {
                    if (!String.IsNullOrWhiteSpace(ingredient.Name))
                    { 
                        Ingredient newIngredient = new Ingredient()
                        {
                            Name = ingredient.Name,
                            Recipe = recipe
                        };

                        _context.Ingredients.Add(newIngredient);
                        _context.SaveChanges();
                    }
                }

                foreach (var step in newRecipe.Steps)
                {
                    if (!String.IsNullOrWhiteSpace(step.Description))
                    {
                        Step newStep = new Step()
                        {
                            Description = step.Description,
                            Recipe = recipe
                        };

                        _context.Steps.Add(newStep);
                        _context.SaveChanges();
                    } 
                }

                if (!string.IsNullOrEmpty(recipe.Tags))
                {
                    string[] tags = recipe.Tags.Split(null);
                    foreach (string tag in tags)
                    {
                        Tag existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);
                        Tagging tagging = new Tagging();

                        if (existingTag != null)
                        {
                            tagging.RecipeID = recipe.RecipeID;
                            tagging.Recipe = recipe;
                            tagging.TagID = existingTag.TagID;
                            tagging.TagName = existingTag.Name;
                            tagging.Tag = existingTag;
                        }
                        else
                        {
                            Tag newTag = new Tag()
                            {
                                Name = tag
                            };
                            _context.Tags.Add(newTag);

                            tagging.RecipeID = recipe.RecipeID;
                            tagging.Recipe = recipe;
                            tagging.TagID = newTag.TagID;
                            tagging.TagName = newTag.Name;
                            tagging.Tag = newTag; 
                        }

                        _context.Taggings.Add(tagging);
                        _context.SaveChanges();
                    }
                }

                return RedirectToAction("Index", "Home");
            }

            var categories = from c in _context.Categories orderby c.Name select c;
            ViewBag.Categories = new SelectList(categories.AsNoTracking(), "CategoryID", "Name", newRecipe.Category);

            var dLevels = from d in _context.DifficultyLevels orderby d.Name select d;
            ViewBag.DifficultyLevels = new SelectList(dLevels.AsNoTracking(), "DifficultyLevelID", "Name", newRecipe.DifficultyLevel);

            return View(newRecipe);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Recipe recipe = await _context.Recipes
                .Include(x => x.Category)
                .Include(x => x.DifficultyLevel)
                .Include(x => x.Steps)
                .Include(x => x.Ingredients)
                .FirstOrDefaultAsync(p => p.RecipeID == id);

            if (recipe != null)
            {
                EditRecipeVM recipeToEdit = new EditRecipeVM()
                {
                    Name = recipe.Name,
                    Description = recipe.Description,
                    Category = recipe.Category.CategoryID,
                    DifficultyLevel = recipe.DifficultyLevel.DifficultyLevelID,
                    Tags = recipe.Tags,
                    OriginalRecipe = id,
                    Steps = recipe.Steps.ToList<Step>(),
                    Ingredients = recipe.Ingredients.ToList<Ingredient>()
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
                Recipe originalRecipe = await _context.Recipes
                    .Include(x => x.Category)
                    .Include(x => x.DifficultyLevel)
                    .Include(x => x.Steps)
                    .Include(x => x.Ingredients)
                    .FirstOrDefaultAsync(p => p.RecipeID == recipe.OriginalRecipe);

                if (originalRecipe != null)
                {
                    originalRecipe.Name = recipe.Name;
                    originalRecipe.Description = recipe.Description;
                    originalRecipe.Category = await _context.Categories.Where(x => x.CategoryID == recipe.Category).SingleAsync();
                    originalRecipe.DifficultyLevel = await _context.DifficultyLevels
                        .Where(x => x.DifficultyLevelID == recipe.DifficultyLevel).SingleAsync();

                    foreach (Ingredient ingredient in originalRecipe.Ingredients)
                    {
                        _context.Ingredients.Remove(ingredient);
                    }

                    foreach (Ingredient ingredient in recipe.Ingredients)
                    {
                        if (!String.IsNullOrWhiteSpace(ingredient.Name))
                        {
                            Ingredient newIngredient = new Ingredient()
                            {
                                Name = ingredient.Name,
                                Recipe = originalRecipe
                            };

                            _context.Ingredients.Add(newIngredient);
                            _context.SaveChanges();
                        }
                    }

                    foreach (Step step in originalRecipe.Steps)
                    {
                        _context.Steps.Remove(step);
                    }

                    foreach (Step step in recipe.Steps)
                    {
                        if (!String.IsNullOrWhiteSpace(step.Description))
                        {
                            Step newStep = new Step()
                            {
                                Description = step.Description,
                                Recipe = originalRecipe
                            };

                            _context.Steps.Add(newStep);
                            _context.SaveChanges();
                        }
                    }

                    string[] originalTags = originalRecipe.Tags.Split(null);
                    string[] newTags = recipe.Tags.Split(null);

                    foreach (string tag in newTags)
                    {
                        if (!originalTags.Contains(tag))
                        {
                            Tag existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tag);
                            Tagging tagging = new Tagging();

                            if (existingTag != null)
                            {
                                tagging.RecipeID = originalRecipe.RecipeID;
                                tagging.Recipe = originalRecipe;
                                tagging.TagID = existingTag.TagID;
                                tagging.TagName = existingTag.Name;
                                tagging.Tag = existingTag;
                            }
                            else
                            {
                                Tag newTag = new Tag()
                                {
                                    Name = tag
                                };
                                _context.Tags.Add(newTag);

                                tagging.RecipeID = originalRecipe.RecipeID;
                                tagging.Recipe = originalRecipe;
                                tagging.TagID = newTag.TagID;
                                tagging.TagName = newTag.Name;
                                tagging.Tag = newTag;
                            }

                            _context.Taggings.Add(tagging);
                            _context.SaveChanges();
                        }
                    }
                    foreach (string tag in originalTags)
                    {
                        if (!newTags.Contains(tag))
                        {
                            Tagging taggingToDelete = await _context.Taggings
                                .FirstOrDefaultAsync(t => t.Recipe == originalRecipe && t.TagName == tag);

                            _context.Taggings.Remove(taggingToDelete);
                            _context.SaveChanges();
                        }
                    }
                    
                    originalRecipe.Tags = Regex.Replace(recipe.Tags, " {2,}", " ");

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
                .Include(r => r.Taggings).ThenInclude(t => t.Tag)
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.RecipeID == id);

            //ViewBag.isAuthor = false;
            //ViewBag.isInFavorites = false;

            RecipeDetailsVM recipeDetailsVM = new RecipeDetailsVM()
            {
                Recipe = recipe,
                IsAuthor = false,
                IsInFavorites = false
            };

            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (recipe.Author != null && recipe.Author.Id == user.Id)
                {
                    //ViewBag.isAuthor = true;
                    recipeDetailsVM.IsAuthor = true;
                }
                else
                {
                    if (_context.FavoriteRecipes.Any())
                    {
                        FavoriteRecipe favoriteRecipe = await _context.FavoriteRecipes
                            .Where(r => r.RecipeID == id && r.UserID == user.Id)
                            .FirstOrDefaultAsync();

                        if (!(favoriteRecipe == null))
                        {
                            //ViewBag.isInFavorites = true;
                            recipeDetailsVM.IsInFavorites = true;

                            //ViewBag.favoriteRecipeID = favoriteRecipe.FavoriteRecipeID;
                            recipeDetailsVM.FavoriteRecipeID = favoriteRecipe.FavoriteRecipeID; ;
                        }
                    }
                }
            }

            //return View(recipe);
            return View(recipeDetailsVM);
        }

        public async Task<IActionResult> AddToFavorites(int id)
        {
            WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
            Recipe recipe = await _context.Recipes.Where(x => x.RecipeID == id).SingleAsync();

            FavoriteRecipe favoriteRecipe = new FavoriteRecipe()
            {
                User = user,
                UserID = user.Id,
                Recipe = recipe,
                RecipeID = id
            };

            _context.FavoriteRecipes.Add(favoriteRecipe);
            _context.SaveChanges();

            return RedirectToAction("Details", "Recipe", new { id = id });
        }

        public async Task<IActionResult> RemoveFromFavorites(int id, int? recipeId, string userId = null)
        {
            FavoriteRecipe favoriteRecipe = await _context.FavoriteRecipes
                .Where(r => r.FavoriteRecipeID == id).SingleAsync();

            //returnId = favoriteRecipe.RecipeID;

            _context.FavoriteRecipes.Remove(favoriteRecipe);
            _context.SaveChanges();

            if (userId != null)
            {
                return RedirectToAction("Details", "Account", new { id = userId });
            }
            return RedirectToAction("Details", "Recipe", new { id = recipeId });
        }
    }
}