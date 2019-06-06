using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class RecipeDetailsVM
    {
        public Recipe Recipe { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsInFavorites { get; set; }
        public int? FavoriteRecipeID { get; set; }
    }
}
