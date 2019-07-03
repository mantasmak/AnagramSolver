using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        NameValueCollection conf;

        [SetUp]
        public void Setup()
        {
            //conf = ConfigurationManager.AppSettings;
        }

        [Test]
        public void FindCorrectAnagramsForGivenWord()
        {
            AnagramSolver anSo = new AnagramSolver();
            string word = "dangus";

            IList<String> anagrams = anSo.GetAnagrams(word);

            Assert.IsTrue(anagrams.Contains("dugnas"));
            Assert.IsTrue(anagrams.Contains("gandus"));
        }
    }
}
