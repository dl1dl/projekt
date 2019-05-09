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
        public string Body { get; set; }
        [Required]
        public int Category { get; set; }
    }
}
