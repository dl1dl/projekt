using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class UserDetails
    {
        public WebAppUser User { get; set; }
        public IQueryable<Recipe> Recipes { get; set; }
    }
}
