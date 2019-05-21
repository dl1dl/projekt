using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;
using projekt.Models.ViewModels;

namespace projekt.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext ctx)
        {
            _context = ctx;
        }

        /*public IActionResult Index()
        {
            return View();
        }*/

        public async Task<IActionResult> Index(string id)
        {
            Tag tag = await _context.Tags
                .Include(t => t.Taggings)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == id);

            TagVM tagVM = new TagVM();

            if (tag != null)
            {
                List<Recipe> taggedRecipes = new List<Recipe>();
                foreach (var recipe in tag.Taggings)
                {
                    taggedRecipes.Add(await _context.Recipes.Include(x => x.Category).FirstOrDefaultAsync(x => x.RecipeID == recipe.RecipeID));
                }
                tagVM.TagName = tag.Name;
                tagVM.Recipes = taggedRecipes;
            }
            else
            {
                tagVM.TagName = id;
                tagVM.Recipes = new List<Recipe>();
            } 

            return View(tagVM);
        }
    }
}