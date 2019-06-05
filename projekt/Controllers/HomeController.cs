using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;
using projekt.Models.ViewModels;

namespace projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly AppDbContext _context;

        public HomeController(AppDbContext ctx, IRecipeRepository rec, ICatRepository cat, IDiffLevelRepository dif, 
            UserManager<WebAppUser> umn, SignInManager<WebAppUser> sim)
        {
            _context = ctx;
            _userManager = umn;
            _signInManager = sim;
        }
        
        public async Task<IActionResult> Index(int id, string searchString)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                ViewBag.IsLogged = true;
            }
            else
            {
                ViewBag.IsLogged = false;
            }

            List<Recipe> recipes = new List<Recipe>();
            Category category = await _context.Categories.Where(x => x.CategoryID == id).FirstOrDefaultAsync();

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                var recipesFromContext = _context.Recipes
                    .Include(i => i.Ingredients)
                    .Include(t => t.Taggings)
                    .Include(c => c.Category);
                //var recipesWithIngredient = _context.Ingredients.Include(r => r.Recipe);
                List<Recipe> recipesToRemove = new List<Recipe>();

                searchString = Regex.Replace(searchString, " {2,}", " ");
                string[] searchArray = searchString.Split(null);

                List<String> tagsToAdd = searchArray.Where(x => x[0] == '#').ToList();
                for (int i = 0; i < tagsToAdd.Count(); i++)
                {
                    tagsToAdd[i] = tagsToAdd[i].Substring(1);
                }

                List<String> tagsToRemove = searchArray.Where(x => x[0] == '-').ToList();
                for (int i = 0; i < tagsToRemove.Count(); i++)
                {
                    tagsToRemove[i] = tagsToRemove[i].Substring(1);
                }

                List<String> words = searchArray.Where(x => x[0] != '#' && x[0] != '-').ToList();

                ViewBag.tagsToAdd = tagsToAdd;
                ViewBag.tagsToRemove = tagsToRemove;
                ViewBag.words = words;
                ViewBag.id = id;

                if (tagsToAdd.Count() > 0)
                {
                    foreach (var tag in tagsToAdd)
                    {
                        if (recipes.Count() == 0)
                        {
                            //recipes.AddRange(recipesFromContext.Where(x => x.Tags.Contains(tag)));
                            recipes.AddRange(recipesFromContext.Where(x => x.Taggings.Where(t => t.TagName == tag).Count() > 0));
                        }
                        else
                        {
                            //var recipesWithTag = recipesFromContext.Where(x => x.Tags.Contains(tag));
                            var recipesWithTag = recipesFromContext
                                .Where(x => x.Taggings.Where(t => t.TagName == tag).Count() > 0);

                            if (recipesWithTag.Count() > 0)
                            {
                                List<Recipe> recipesAdded = new List<Recipe>();
                                recipesAdded.AddRange(recipes);
                                recipes = recipesAdded.Intersect(recipesWithTag).ToList();
                            }
                        }
                    }
                }
                else
                {
                    recipes = recipesFromContext.ToList();
                }

                /*foreach (var word in words)
                {
                    if (recipesToRemove.Count() == 0)
                    {
                        recipesToRemove.AddRange(recipes.Where(x => !x.Name.Contains(word) && !x.Description.ToLower().Contains(word) 
                            && (x.Ingredients.Where(z => z.Name.ToLower().Contains(word)).Count() == 0) ).ToList());
                    }
                    else
                    {
                        List<Recipe> newRecipesToRemove = recipes.Where(x => !x.Name.Contains(word) && !x.Description.ToLower().Contains(word)
                            && (x.Ingredients.Where(z => z.Name.ToLower().Contains(word)).Count() == 0) ).ToList();

                        if (newRecipesToRemove.Count() > 0)
                        {
                            List<Recipe> oldRecipesToRemove = new List<Recipe>();
                            oldRecipesToRemove.AddRange(recipesToRemove);

                            recipesToRemove = newRecipesToRemove.Intersect(oldRecipesToRemove).ToList();
                        }
                    }
                }*/

                foreach (var word in words)
                {
                    if (!tagsToAdd.Contains(word))
                    {
                        recipesToRemove.AddRange(recipes.Where(x => !x.Name.Contains(word) && !x.Description.ToLower().Contains(word)
                            && (x.Ingredients.Where(z => z.Name.ToLower().Contains(word)).Count() == 0)).ToList());
                    }
                }
                if (recipesToRemove.Count() > 0)
                {
                    foreach (var recipeToRemove in recipesToRemove)
                    {
                        recipes.Remove(recipeToRemove);
                    }
                }

                foreach (var tag in tagsToRemove)
                {
                    //recipes.RemoveAll(x => x.Tags.Contains(tag));
                    recipesToRemove = recipes.Where(x => x.Taggings.Where(t => t.TagName == tag).Count() > 0).ToList();
                    foreach (Recipe recipe in recipesToRemove)
                    {
                        recipes.Remove(recipe);
                    }
                }
            }
            else
            {
                recipes = await _context.Recipes.ToListAsync();
            }
            
            if (id != 0)
            {
                /*List<Recipe> recipesInCategory = new List<Recipe>();
                foreach (var recipe in recipes)
                {
                    if (recipe.Category == category)
                    {
                        recipesInCategory.Add(recipe);
                    }
                }
                recipes = recipesInCategory;*/
                recipes.RemoveAll(x => x.Category != category);
            }

            IndexVM IndexVM = new IndexVM
            {
                Recipes = recipes.Distinct().ToList(),
                CategoryID = id
            };

            return View(IndexVM);
        }

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