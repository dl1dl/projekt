using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class NewRecipeVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Category { get; set; }
        [Required]
        public int DifficultyLevel { get; set; }
        public string Tags { get; set; }
        public List<Step> Steps { get; set; }
    }
}
