using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;
using projekt.Models.ViewModels;

namespace projekt.Components
{
    public class AddCommentViewComponent : ViewComponent
    {
        private readonly SignInManager<WebAppUser> _signInManager;
        private readonly UserManager<WebAppUser> _userManager;

        public AddCommentViewComponent(SignInManager<WebAppUser> sim, UserManager<WebAppUser> umn)
        {
            _signInManager = sim;
            _userManager = umn;
        }

        public async Task<IViewComponentResult> InvokeAsync(int recipeID)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                WebAppUser user = await _userManager.GetUserAsync(HttpContext.User);
                NewCommentVM newComment = new NewCommentVM()
                {
                    Author = user.Id,
                    Recipe = recipeID
                };

                return View("Logged", newComment);
            }
            else
            {
                return View();
            }
        }
    }
}
