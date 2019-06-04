using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projekt.Models.ViewModels
{
    public class SearchVM
    {
        public string SearchString { get; set; }
        public string Tags { get; set; }
        public int Category { get; set; }
        public int DifficultyLevel { get; set; }
    }
}
