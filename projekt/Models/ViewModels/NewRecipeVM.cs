using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class NewRecipeVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Category { get; set; }
        [Required]
        public int DifficultyLevel { get; set; }
        [Required]
        public string Tags { get; set; }
        [Required]
        public List<Step> Steps { get; set; }
        [Required]
        public List<Ingredient> Ingredients { get; set; }
    }
}
