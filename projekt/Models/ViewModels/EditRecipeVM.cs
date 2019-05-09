using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class EditRecipeVM : NewRecipeVM
    {
        public int OriginalRecipe { get; set; }
    }
}
