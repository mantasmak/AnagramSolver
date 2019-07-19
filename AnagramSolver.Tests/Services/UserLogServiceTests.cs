using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using NSubstitute;
using System.Linq;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class UserLogServiceTests
    {
        private IUserLogRepository userLogRepository;
        private UserLogService userLogService;

        [SetUp]
        public void Setup()
        {
            userLogRepository = Substitute.For<IUserLogRepository>();
            userLogService = new UserLogService(userLogRepository);
        }

        [Test]
        public void GenerateUserLogReport_ShouldGenerateCorrectUserLogReport()
        {
            userLogRepository.GetUserLogReport().Returns(new List<UserLogReport>
            {
                new UserLogReport {UserIp = "107.0.0.1", SearchTime = new DateTime(2019, 7, 19), Word = "dangus", Anagrams = new List<string> {"dugnas"}},
                new UserLogReport {UserIp = "107.0.0.1", SearchTime = new DateTime(2019, 7, 19), Word = "dangus", Anagrams = new List<string> {"gandus"}}
            });

            List<string> expected = new List<string> { "dugnas", "gandus" };

            var result = userLogService.GenerateUserLogReport();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result.First().Anagrams);

            userLogRepository.Received().GetUserLogReport();
        }
    }
}
