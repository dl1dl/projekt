﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index(string searchString)
        {
            if (_signInManager.IsSignedIn(HttpContext.User))
            {
                ViewBag.IsLogged = true;
            }
            else
            {
                ViewBag.IsLogged = false;
            }

            var recipes = from r in _context.Recipes select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Name.Contains(searchString) || r.Description.Contains(searchString));
            }

            IndexVM IndexVM = new IndexVM
            {
                Recipes = recipes,
                Categories = _context.Categories,
                DiffLevels = _context.DifficultyLevels
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