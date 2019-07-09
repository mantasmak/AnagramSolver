using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using MainApp.WebApp.Models;
using Implementation.AnagramSolver;
using Interfaces.AnagramSolver;
using Microsoft.AspNetCore.Http;

namespace MainApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        IAnagramSolver anagramSolver;
        IWordRepository fileWordReader;

        public HomeController(IAnagramSolver anagramSolver, IWordRepository fileWordReader)
        {
            this.anagramSolver = anagramSolver;
            this.fileWordReader = fileWordReader;
        }

        public IActionResult Index(string word)
        {
            AddCookie();      //Commented for tests to pass
            if (word == null)
                return new EmptyResult();
    
            WordViewModel wordViewModel = new WordViewModel();
            wordViewModel.Name = word;
            wordViewModel.Anagrams = anagramSolver.GetAnagrams(word);

            return View(wordViewModel);
        }

        public IActionResult ListOfWords(int startIndex, int pageSize = 100)
        {
            WordContainerViewModel wordContainerViewModel = new WordContainerViewModel();
            var wordsToDisplay = fileWordReader.ReadWords().SelectMany(d => d.Value)
                .Skip(startIndex)
                .Take(pageSize)
                .ToList();

            wordContainerViewModel.Words = wordsToDisplay;

            ViewData["nextPageIndex"] = startIndex + pageSize;
            ViewData["previousPageIndex"] = startIndex - pageSize;

            return View(wordContainerViewModel);
        }

        public IActionResult WriteCookies()
        {

            return View();
        }

        public void AddCookie()
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            option.IsEssential = true;
            Response.Cookies.Append("Time", DateTime.Now.ToString(), option);
        }

        public IActionResult SearchWord(string searchString)
        {
            ViewData["searchString"] = searchString;
            DatabaseWordReader dbReader = new DatabaseWordReader();
            List<string> anagrams = dbReader.FindAnagrams(searchString).ToList();

            return View(anagrams);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
