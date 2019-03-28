using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt.Models;

namespace projekt.Data
{
    public class EFDiffLevelRepository : IDiffLevelRepository
    {
        private AppDbContext _context;

        public EFDiffLevelRepository(AppDbContext con)
        {
            _context = con;
        }

        public IQueryable<DifficultyLevel> DifficultyLevels => _context.DifficultyLevels;
    }
}
