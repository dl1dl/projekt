using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Components
{
    public class FavRecipesInProfileViewComponent : ViewComponent
    {
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly AppDbContext _context;

        public FavRecipesInProfileViewComponent(AppDbContext ctx, SignInManager<WebAppUser> sim, UserManager<WebAppUser> umn)
        {
            _context = ctx;
            _signInManager = sim;
            _userManager = umn;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
                if (user.Id == userId)
                {
                    List<FavoriteRecipe> favoriteRecipes = _context.FavoriteRecipes
                                                            .Include(u => u.User)
                                                            .Include(r => r.Recipe)
                                                            .Where(x => x.User == user).ToList();

                    /*List<Recipe> recipes = new List<Recipe>();
                    foreach (FavoriteRecipe favoriteRecipe in favoriteRecipes)
                    {
                        recipes.Add(favoriteRecipe.Recipe);
                    }*/

                    return View("User", favoriteRecipes);
                }
            }
            return View();
        }
    }
}
