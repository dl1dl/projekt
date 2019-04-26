using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using projekt.Models;

namespace projekt.Data
{
    public class EFRecipeRepository : IRecipeRepository
    {
        private AppDbContext _context;

        public EFRecipeRepository(AppDbContext con)
        {
            _context = con;
        }

        public IQueryable<Recipe> Recipes => _context.Recipes.Include(r => r.Author).ThenInclude(r => r.Recipes);

        public void AddRecipe(Recipe recipe)
        {
            _context.Add(recipe);
            _context.SaveChanges();
        }

        public void EditRecipe(Recipe recipe)
        {
            Recipe originalRecipe = _context.Recipes.FirstOrDefault(p => p.RecipeID == recipe.RecipeID);
            if (originalRecipe != null)
            {
                originalRecipe.Name = recipe.Name;
                originalRecipe.Body = recipe.Body;
            }
            _context.SaveChanges();
        }

        public void DeleteRecipe(Recipe recipe)
        {
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                _context.SaveChanges();
            }
        }
    }
}
