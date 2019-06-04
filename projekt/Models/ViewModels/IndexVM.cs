using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class IndexVM
    {
        public List<Recipe> Recipes { get; set; }
        //public List<Category> Categories { get; set; }
        //public List<DifficultyLevel> DiffLevels { get; set; }
        public Category Category { get; set; }
        public int CategoryID { get; set; }
        public Tag Tag { get; set; }

    }
}
