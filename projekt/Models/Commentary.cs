using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Commentary
    {
        public int CommentaryID { get; set; }
        public int AuthorId { get; set; }
        public int RecipeId { get; set; }

        public string Body { get; set; }
    }
}
    