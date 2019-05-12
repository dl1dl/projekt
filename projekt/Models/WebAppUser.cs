using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace projekt.Models
{
    public class WebAppUser : IdentityUser
    {
        public ICollection<Recipe> Recipes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }
}
