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
        public string Name { get; set; }

        public WebAppUser Author { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public DifficultyLevel DifficultyLevel { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
        public string Body { get; set; }
    }
}
