using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projekt.Data;
using projekt.Models;

namespace projekt.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext ctx)
        {
            _context = ctx;
        }

        public async Task<IActionResult> Index()
        {
            var categories = from c in _context.Categories select c;

            return View(await categories.ToListAsync());
        }

        public async Task<IActionResult> All(int categoryID)
        {

            Category category = await _context.Categories
                .Include(c => c.Recipes)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.CategoryID == categoryID);

            return View(category);
        }
    }
}