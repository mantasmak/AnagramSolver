using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Implementation.AnagramSolver;
using Contracts;
using Microsoft.AspNetCore.Http;

namespace MainApp.WebApp.Controllers
{
    public class DataController : Controller
    {
        IAnagramSolver anagramSolver;
        IWordRepository fileWordReader;
        IHttpContextAccessor contextAccessor;

        public DataController(IAnagramSolver anagramSolver, IWordRepository fileWordReader, IHttpContextAccessor contextAccessor)
        {
            this.anagramSolver = anagramSolver;
            this.fileWordReader = fileWordReader;
            this.contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetAnagrams(string word)
        {
            IList<string> anagrams = anagramSolver.GetAnagrams(word, contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString());
            string json = JsonConvert.SerializeObject(anagrams);
            return json;
        }

        public string GetDictionary()
        {
            var wordsToDisplay = fileWordReader.GetAllWords();
            string json = JsonConvert.SerializeObject(wordsToDisplay);

            return json;
        }


    }
}