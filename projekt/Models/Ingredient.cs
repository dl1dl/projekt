using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public Recipe Recipe { get; set; }
    }
}
