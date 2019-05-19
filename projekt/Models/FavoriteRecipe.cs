using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class FavoriteRecipe
    {
        public int FavoriteRecipeID { get; set; }

        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        public string UserID { get; set; }
        public WebAppUser User { get; set; }
    }
}
