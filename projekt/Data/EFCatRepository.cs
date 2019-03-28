using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;

namespace projekt.Data
{
    public class EFCatRepository : ICatRepository
    {
        private AppDbContext _context;

        public EFCatRepository(AppDbContext con)
        {
            _context = con;
        }

        public IQueryable<Category> Categories => _context.Categories;
    }
}
