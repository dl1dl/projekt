using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public WebAppUser Author { get; set; }
        public Recipe Recipe { get; set; }
        public string Body { get; set; }
    }
}
    