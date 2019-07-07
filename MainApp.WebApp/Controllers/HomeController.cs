using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(IAnagramSolver anagramSolver)
        {
            this.anagramSolver = anagramSolver;
        }
        /**
        public IActionResult Index()
        {
            
            return View();
        }
    **/
        public IActionResult Index(string word)
        {
            if (word == null)
                return new EmptyResult();
    
            WordViewModel wordViewModel = new WordViewModel();
            wordViewModel.Name = word;
            wordViewModel.Anagrams = anagramSolver.GetAnagrams(word);

            return View(wordViewModel);
        }

        //public IActionResult 

        public IActionResult ListOfWords(int startIndex, int pageSize = 100)
        {
            WordContainerViewModel wordContainerViewModel = new WordContainerViewModel();
            FileWordReader reader = new FileWordReader();
            var wordsToDisplay = reader.ReadWords().SelectMany(d => d.Value)
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
            SaveCookies();
            ViewData["Cookie"] = Request.Cookies["Time"];

            return View();
        }

        public IActionResult SaveCookies()
        {
            var option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("Time", "erwgerg", option);

            return View();
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
