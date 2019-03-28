using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public int AuthorID { get; set; }
        public int CategoryId { get; set; }
        public int DiffLevelId { get; set; }

        public string Name { get; set; }
        public string Body { get; set; }
    }
}
