using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }

        [Required]
        public string Name { get; set; }

        public WebAppUser Author { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public DifficultyLevel DifficultyLevel { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
        [Required]
        public string Tags { get; set; }
        public ICollection<Tagging> Taggings { get; set; }

        public string Description { get; set; }
        [Required]
        public ICollection<Step> Steps { get; set; }
        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }
    }
}