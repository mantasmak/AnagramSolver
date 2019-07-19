using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using MainApp.WebApp.Models;
using Implementation.AnagramSolver;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MainApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        IAnagramSolver anagramService;
        IWordsManipulator wordManipulator;
        IUserLogService userLogService;
        IWordService wordService;


        public HomeController(IAnagramSolver anagramService, IWordsManipulator wordManipulator, IUserLogService userLogService, IWordService wordService)
        {
            this.anagramService = anagramService;
            this.wordManipulator = wordManipulator;
            this.userLogService = userLogService;
            this.wordService = wordService;
        }

        public IActionResult Index(string word)
        {
            AddCookie();
            if (word == null)
                return new EmptyResult();
    
            WordViewModel wordViewModel = new WordViewModel();
            wordViewModel.Name = word;
            string ip = HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
            wordViewModel.Anagrams = anagramService.GetAnagrams(word, ip);

            return View(wordViewModel);
        }

        public IActionResult ListOfWords(int startIndex, int pageSize = 100)
        {
            WordContainerViewModel wordContainerViewModel = new WordContainerViewModel();
            var wordsToDisplay = wordService.GetAllWords()
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
            List<string> anagrams = wordService.RecognizeWord(searchString).ToList();

            return View(anagrams);
        }

        public IActionResult UserLogs()
        {
            List<UserLogReport> reports = userLogService.GenerateUserLogReport();

            return View(reports);
        }

        [HttpGet]
        public IActionResult ManipulateWord()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddWord(WordManipulationViewModel input)
        {
            if(wordManipulator.AddWord(input.Word, HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString()))
            {
                return RedirectToAction("ManipulateWordResult", new { message = "Word added succesfully" });
            }
            else
            {
                return RedirectToAction("ManipulateWordResult", new { message = "Could not add word" });
            }

        }

        public IActionResult ManipulateWordResult(string message)
        {
            ManipulateWordResultViewModel model = new ManipulateWordResultViewModel();
            model.Message = message;

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteWord(WordManipulationViewModel input)
        {
            if(wordManipulator.RemoveWord(input.Word, HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString()))
            {
                return RedirectToAction("ManipulateWordResult", new { message = "Word deleted succesfully" });
            }
            else
            {
                return RedirectToAction("ManipulateWordResult", new { message = "Could not delete word" });
            }
        }

        [HttpPost]
        public IActionResult CorrectWord(WordManipulationViewModel input)
        {
            if(wordManipulator.UpdateWord(input.Word, input.WordCorrection, HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString()))
            {
                return RedirectToAction("ManipulateWordResult", new { message = "Word corrected succesfully" });
            }
            else
            {
                return RedirectToAction("ManipulateWordResult", new { message = "Could not correct word" });
            }
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
