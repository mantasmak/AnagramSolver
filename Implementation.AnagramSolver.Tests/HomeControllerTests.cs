using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using MainApp.WebApp.Controllers;
using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Implementation.AnagramSolver.Tests
{
    class HomeControllerTests
    {
        Mock<IAnagramSolver> anagramSolver;
        Mock<IWordRepository> wordRepository;
        IConfiguration Config { get; set; }

        [SetUp]
        public void Setup()
        {
            anagramSolver = new Mock<IAnagramSolver>();
            wordRepository = new Mock<IWordRepository>();
            Config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            wordRepository.Setup(x => x.ReadWords())
               .Returns(new Dictionary<string, List<string>> { ["adgnsu"] = new List<string> { "dangus", "dugnas", "gandus" } });
            anagramSolver.Setup(x => x.GetAnagrams("dangus")).Returns(new List<string> { "dugnas", "gandus" });
        }

        [Test]
        public void ReturnEmptyResultWhenNoWordsPassed()
        {
            HomeController homeController = new HomeController(anagramSolver.Object, wordRepository.Object);

            var result = homeController.Index(null);

            Assert.IsInstanceOf<EmptyResult>(result);
        }

        [Test]
        public void IndexReturnsCorrectViewWhenWordIsPassed()
        {
            HomeController homeController = new HomeController(anagramSolver.Object, wordRepository.Object);

            var result = homeController.Index(Config["testWord1"]);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void ListOfWordsReturnsView()
        {           
            HomeController homeController = new HomeController(anagramSolver.Object, wordRepository.Object);

            var result = homeController.ListOfWords(0);

            Assert.IsInstanceOf<ViewResult>(result);
        }

        
    }
}
