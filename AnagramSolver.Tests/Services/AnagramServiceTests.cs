using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Contracts;
using NSubstitute;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Implementation.AnagramSolver.Tests.Services
{
    [TestFixture]
    class AnagramServiceTests
    {
        private IUserLogRepository userLogRepository;
        private IWordRepository wordRepository;
        private ICacheRepository cacheRepository;
        private INumOfAllowedSearchesRepository allowedSearchesRepository;
        private IOptions<AnagramServiceOptions> options;
        private AnagramService anagramService;

        [SetUp]
        public void Setup()
        {
            userLogRepository = Substitute.For<IUserLogRepository>();
            wordRepository = Substitute.For<IWordRepository>();
            cacheRepository = Substitute.For<ICacheRepository>();
            allowedSearchesRepository = Substitute.For<INumOfAllowedSearchesRepository>();
            AnagramServiceOptions anagramServiceOptions = new AnagramServiceOptions() { MaxListLen = 10 };
            options = Options.Create(anagramServiceOptions);
            anagramService = new AnagramService(wordRepository, cacheRepository, allowedSearchesRepository, options, userLogRepository);
        }

        [Test]
        public void GetAnagrams_ShouldReturnExistingAnagrams()
        {
            string word = "dangus"; 
            string ip = "107.0.0.1";

            allowedSearchesRepository.CheckIfExists(ip).Returns(true);
            allowedSearchesRepository.GetAmountOfSearches(ip).Returns(10);
            userLogRepository.CountUserSearchesByIp(ip).Returns(1);
            cacheRepository.GetCachedAnagrams(word).Returns(new List<string> { "dugnas", "gandus" });

            var result = anagramService.GetAnagrams(word, ip);

            Assert.IsNotNull(result);
            Assert.AreEqual("dugnas", result.First());

            allowedSearchesRepository.Received().CheckIfExists(ip);
            allowedSearchesRepository.Received().GetAmountOfSearches(ip);
            userLogRepository.Received().CountUserSearchesByIp(ip);
            cacheRepository.Received().GetCachedAnagrams(word);

            allowedSearchesRepository.DidNotReceive().SaveNewUser(Arg.Any<string>(), Arg.Any<int>());
            wordRepository.DidNotReceive().FindAnagrams(Arg.Any<string>());
            cacheRepository.DidNotReceive().Save(Arg.Any<string>(), Arg.Any<IEnumerable<string>>());
        }
    }
}
