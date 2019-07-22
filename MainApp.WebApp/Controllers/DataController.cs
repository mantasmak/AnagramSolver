using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Contracts;
using Microsoft.AspNetCore.Http;

namespace MainApp.WebApp.Controllers
{
    public class DataController : Controller
    {
        IAnagramSolver anagramSolver;
        IWordService wordService;

        public DataController(IAnagramSolver anagramSolver, IWordService wordService)
        {
            this.anagramSolver = anagramSolver;
            this.wordService = wordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetAnagrams(string word)
        {
            IList<string> anagrams = anagramSolver.GetAnagrams(word, HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString());
            string json = JsonConvert.SerializeObject(anagrams);
            return json;
        }

        public string GetDictionary()
        {
            var wordsToDisplay = wordService.GetAllWords();
            string json = JsonConvert.SerializeObject(wordsToDisplay);

            return json;
        }
    }
}