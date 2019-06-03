using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class IndexVM
    {
        public IQueryable<Recipe> Recipes { get; set; }
        public IQueryable<Category> Categories { get; set; }
        public IQueryable<DifficultyLevel> DiffLevels { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }
    }
}
