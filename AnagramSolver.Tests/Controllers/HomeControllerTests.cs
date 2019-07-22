using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using NUnit.Framework;
using NSubstitute;
using MainApp.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Implementation.AnagramSolver.Tests.Controllers
{
    [TestFixture]
    class HomeControllerTests
    {
        private IAnagramSolver anagramService;
        private IWordsManipulator wordManupulator;
        private IUserLogService userLogService;
        private IWordService wordService;
        private HomeController homeController;
        HttpResponse httpResponse;
        HttpContext httpContext;
        ITempDataDictionary tempData;

        [SetUp]
        public void Setup()
        {
            anagramService = Substitute.For<IAnagramSolver>();
            wordManupulator = Substitute.For<IWordsManipulator>();
            userLogService = Substitute.For<IUserLogService>();
            wordService = Substitute.For<IWordService>();
            httpResponse = Substitute.For<HttpResponse>();
            httpContext = Substitute.For<HttpContext>();
            tempData = Substitute.For<ITempDataDictionary>();
            homeController = new HomeController(anagramService, wordManupulator, userLogService, wordService) { TempData = tempData };
            homeController.ControllerContext = new ControllerContext() { HttpContext = httpContext };
        }

        [Test]
        public void Index_ShouldReturnAView()
        {
            string word = "dangus";
            string ip = "107.0.0.1";
            var headerDictionary = new HeaderDictionary();
            httpResponse.Headers.Returns(headerDictionary);
            httpContext.Response.Returns(httpResponse);
            httpContext.Connection.LocalIpAddress.Returns(new System.Net.IPAddress(new Byte[] { 107, 0, 0, 1 }));
            anagramService.GetAnagrams(word, ip).Returns(new List<string> { "dugnas", "gandus" });

            var result = homeController.Index(word);

            Assert.IsInstanceOf<ViewResult>(result);

            anagramService.Received().GetAnagrams(word, ip);
        }

        [Test]
        public void ListOfWords_ShouldReturnAView()
        {
            int startIndex = 0;

            wordService.GetAllWords().Returns(new List<string> { "dangus", "dugnas", "gandus" });

            var result = homeController.ListOfWords(startIndex);

            Assert.IsInstanceOf<ViewResult>(result);

            wordService.Received().GetAllWords();
        }

        [Test]
        public void SearchWord_ShouldReturnAView()
        {
            string searchString = "dan";

            wordService.RecognizeWord(searchString).Returns(new List<string> { "dangus", "dugnas", "gandus" });

            var result = homeController.SearchWord(searchString);

            Assert.IsInstanceOf<ViewResult>(result);

            wordService.Received().RecognizeWord(searchString);
        }

        [Test]
        public void UserLogs_ShouldReturnAView()
        {
            userLogService.GenerateUserLogReport().Returns(new List<UserLogReport> { new UserLogReport
            {
                UserIp = "107.0.0.1",
                SearchTime = new DateTime(2019, 7, 19),
                Word = "dangus",
                Anagrams = new List<string> { "dugnas" }
            } });

            var result = homeController.UserLogs();

            Assert.IsInstanceOf<ViewResult>(result);

            userLogService.Received().GenerateUserLogReport();
        }
    }
}
