using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using NUnit.Framework;
using NSubstitute;
using MainApp.WebApp.Controllers;
using Newtonsoft.Json;

namespace Implementation.AnagramSolver.Tests.Controllers
{
    class DataControllerTests
    {
        IAnagramSolver anagramService;
        IWordService wordService;
        DataController dataController;

        [SetUp]
        public void Setup()
        {
            anagramService = Substitute.For<IAnagramSolver>();
            wordService = Substitute.For<IWordService>();
            dataController = new DataController(anagramService, wordService);
        }

        [Test]
        public void GetDictionary_ShouldReturnAllWords()
        {
            List<string> words = new List<string> { "dangus", "veidas", "langas" };
            string expected = JsonConvert.SerializeObject(words);

            wordService.GetAllWords().Returns(words);

            var result = dataController.GetDictionary();

            Assert.AreEqual(expected, result);

            wordService.Received().GetAllWords();
        }

        [Test]
        public void GetAnagrams_ShouldReturnCorrectAnagrams()
        {
            string word = "dangus";
            string ip = "107.0.0.1";
            List<string> anagrams = new List<string> { "dugnas", "gandus" };
            string expected = JsonConvert.SerializeObject(anagrams);

            anagramService.GetAnagrams(word, ip).Returns(anagrams);

            var result = dataController.GetAnagrams(word);

            Assert.AreEqual(expected, result);

            anagramService.Received().GetAnagrams(word, ip);
        }
    }
}
