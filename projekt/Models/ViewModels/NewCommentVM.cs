using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class NewCommentVM
    {
        public string Author { get; set; }
        public int Recipe { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
