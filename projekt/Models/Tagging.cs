using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Tagging
    {
        public int TaggingID { get; set; }

        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }

        public int TagID { get; set; }
        public string TagName { get; set; }
        public Tag Tag { get; set; }
    }
}
