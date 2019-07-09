using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;

namespace MainApp.WebApp.Controllers
{
    public class DataController : Controller
    {
        IAnagramSolver anagramSolver;
        IWordRepository fileWordReader;

        public DataController(IAnagramSolver anagramSolver, IWordRepository fileWordReader)
        {
            this.anagramSolver = anagramSolver;
            this.fileWordReader = fileWordReader;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetAnagrams(string word)
        {
            IList<string> anagrams = anagramSolver.GetAnagrams(word);
            string json = JsonConvert.SerializeObject(anagrams);
            return json;
        }

        public string GetDictionary()
        {
            var wordsToDisplay = fileWordReader.ReadWords().SelectMany(d => d.Value)
                .ToList();
            string json = JsonConvert.SerializeObject(wordsToDisplay);

            return json;
        }


    }
}