using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;

namespace projekt.Components
{
    public class FavoriteRecipesViewComponent : ViewComponent
    {
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;

        public FavoriteRecipesViewComponent(SignInManager<WebAppUser> sim, UserManager<WebAppUser> umn)
        {
            _signInManager = sim;
            _userManager = umn;
        }

        public async Task<IViewComponentResult> InvokeAsync(int recipeID, int? favoriteRecipeID = null, bool isAuthor = false)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                if (!isAuthor)
                {
                    ViewBag.recipeID = recipeID;

                    if (favoriteRecipeID != null)
                    {
                        ViewBag.favoriteRecipeID = favoriteRecipeID;
                        return View("Remove");
                    }
                    return View("Add");
                }
            }
            return View();
        }
    }
}
