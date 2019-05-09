using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;
using projekt.Models.ViewModels;

namespace projekt.Controllers
{
    public class CommentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<WebAppUser> _userManager;
        private readonly SignInManager<WebAppUser> _signInManager;

        public CommentController(AppDbContext ctx, IRecipeRepository rec, UserManager<WebAppUser> umn, SignInManager<WebAppUser> sim)
        {
            _context = ctx;
            _userManager = umn;
            _signInManager = sim;
        }

        [HttpPost]
        public async Task<IActionResult> Add(NewCommentVM newComment)
        {
            if (ModelState.IsValid)
            {
                Comment comment = new Comment()
                {
                    Author = await _userManager.GetUserAsync(HttpContext.User),
                    Recipe = await _context.Recipes.Where(x => x.RecipeID == newComment.Recipe).SingleAsync(),
                    Body = newComment.Body
                };
                _context.Comments.Add(comment);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", "Recipe", new { id = newComment.Recipe});
        }
    }
}