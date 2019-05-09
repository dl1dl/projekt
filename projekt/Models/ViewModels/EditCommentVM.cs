using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class EditCommentVM
    {
        public string Body { get; set; }
        public string Author { get; set; }
        public int OriginalComment { get; set; }
    }
}
