using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IQueryable<Recipe> Recipes => _context.Recipes;

        public void AddRecipe(Recipe recipe)
        {
            _context.Add(recipe);
            _context.SaveChanges();
        }
    }
}
