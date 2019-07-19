using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using NSubstitute;
using System.Linq;

namespace Implementation.AnagramSolver.Tests.Services
{
    [TestFixture]
    class WordServiceTests
    {
        private IWordRepository wordRepository;
        private WordService wordService;

        [SetUp]
        public void Setup()
        {
            wordRepository = Substitute.For<IWordRepository>();
            wordService = new WordService(wordRepository);
        }

        [Test]
        public void GetAllWords_ShouldGetAllWords()
        {
            wordRepository.GetAllWords().Returns(new List<string>
            {
                "langas",
                "stalas",
                "dangus"
            });

            var result = wordService.GetAllWords();

            Assert.IsNotNull(result);
            Assert.AreEqual("langas", result.First());

            wordRepository.Received().GetAllWords();
        }

        [Test]
        public void RecognizeWord_ShouldReturnRecognizedWords()
        {
            wordRepository.Find("as").Returns(new List<string>
            {
                "langas",
                "stalas",
                "pastatas"
            });

            var result = wordService.RecognizeWord("as");

            Assert.IsNotNull(result);
            Assert.AreEqual("langas", result.First());

            wordRepository.Received().Find("as");
        }
    }
}
