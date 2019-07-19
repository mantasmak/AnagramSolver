using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Contracts;
using NSubstitute;

namespace Implementation.AnagramSolver.Tests.Services
{
    [TestFixture]
    class WordsManipulationServiceTests
    {
        private IWordRepository wordRepository;
        private INumOfAllowedSearchesRepository allowedSearchesRepository;
        private WordsManipulationService wordsManipulationService;

        [SetUp]
        public void Setup()
        {
            wordRepository = Substitute.For<IWordRepository>();
            allowedSearchesRepository = Substitute.For<INumOfAllowedSearchesRepository>();
            wordsManipulationService = new WordsManipulationService(wordRepository, allowedSearchesRepository);
        }

        [Test]
        public void AddWord_ShouldReturnTrue()
        {
            string word = "dangus";
            string ip = "107.0.0.1";

            wordRepository.WordExists(word).Returns(false);

            var result = wordsManipulationService.AddWord(word, ip);

            Assert.IsTrue(result);

            wordRepository.Received().WordExists(word);
            wordRepository.Received().Save(word);
            allowedSearchesRepository.Received().IncrementNumOfAllowedSearches(ip);
        }

        [Test]
        public void RemoveWord_ShouldReturnTrue()
        {
            string word = "dangus";
            string ip = "107.0.0.1";

            wordRepository.WordExists(word).Returns(true);

            var result = wordsManipulationService.RemoveWord(word, ip);

            Assert.IsTrue(result);

            wordRepository.Received().WordExists(word);
            wordRepository.Received().Delete(word);
            allowedSearchesRepository.Received().DecrementNumOfAllowedSearches(ip);
        }

        [Test]
        public void UpdateWord_ShouldReturnTrue()
        {
            string currentWord = "dangus";
            string updatedWord = "dangaus";
            string ip = "107.0.0.1";

            wordRepository.WordExists(currentWord).Returns(true);

            var result = wordsManipulationService.UpdateWord(currentWord, updatedWord, ip);

            Assert.IsTrue(result);

            wordRepository.Received().WordExists(currentWord);
            wordRepository.Received().Update(currentWord, updatedWord);
            allowedSearchesRepository.Received().IncrementNumOfAllowedSearches(ip);
        }
    }
}
