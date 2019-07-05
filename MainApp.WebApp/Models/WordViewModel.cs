using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainApp.WebApp.Models
{
    public class WordViewModel
    {
        public string Name { get; set; }
        public IList<string> Anagrams { get; set; }
    }
}
