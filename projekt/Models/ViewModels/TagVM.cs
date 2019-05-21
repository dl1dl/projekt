using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class TagVM
    {
        public string TagName { get; set; }
        public List<Recipe> Recipes { get; set; }
    }
}
