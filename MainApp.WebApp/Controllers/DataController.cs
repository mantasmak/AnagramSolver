using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Implementation.AnagramSolver;

namespace MainApp.WebApp.Controllers
{
    public class DataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string GetAnagrams(string word)
        {
            AnagramSolver anagramSolver = new AnagramSolver();
            IList<string> anagrams = anagramSolver.GetAnagrams(word);
            string json = JsonConvert.SerializeObject(anagrams);
            return json;
        }

        public string GetDictionary()
        {
            FileWordReader reader = new FileWordReader();
            var wordsToDisplay = reader.ReadWords().SelectMany(d => d.Value)
                .ToList();
            string json = JsonConvert.SerializeObject(wordsToDisplay);

            return json;
        }
    }
}