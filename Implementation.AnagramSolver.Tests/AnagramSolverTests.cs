using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class AnagramSolverTests
    {
        IConfiguration Config { get; set; }
        string Path { get; set; }

        [SetUp]
        public void Setup()
        {
            Config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            Path = Config["testFilePath"];
        }

        [Test]
        public void FindCorrectAnagramsForGivenWord()
        {
            AnagramSolver Solver = new AnagramSolver(Path);
            string word = Config["testWord1"];

            IList<string> anagrams = Solver.GetAnagrams(word);

            Assert.IsTrue(anagrams.Contains("dugnas"));
            Assert.IsTrue(anagrams.Contains("gandus"));
        }

        [Test]
        public void ReturnNullIfNoWordsWhereFound()
        {
            AnagramSolver Solver = new AnagramSolver(Path);
            string word = Config["testWord2"];

            IList<string> anagrams = Solver.GetAnagrams(word);

            Assert.IsNull(anagrams);
        }

        [Test]
        public void DoNotReturnTestWordAsAnagram()
        {
            AnagramSolver Solver = new AnagramSolver(Path);
            string word = Config["testWord3"];

            IList<string> anagrams = Solver.GetAnagrams(word);

            Assert.IsFalse(anagrams.Contains(word));
        }

        [Test]
        public void ReturnListOfCorrectLength()
        {
            int maxListLength = Int32.Parse(Config["maxListLength1"]);
            AnagramSolver Solver = new AnagramSolver(maxListLength, Path);
            string word = Config["testWord1"];
            
            IList<string> anagrams = Solver.GetAnagrams(word);

            Assert.IsTrue(anagrams.Count == maxListLength);
        }

        [Test]
        public void SetCorrectMaxListLengthValueThroughConstructors()
        {
            int maxListLength = Int32.Parse(Config["maxListLength2"]);
            AnagramSolver Solver1 = new AnagramSolver(maxListLength);
            AnagramSolver Solver2 = new AnagramSolver(maxListLength);

            Assert.AreEqual(maxListLength, Solver1.MaxListLen);
            Assert.AreEqual(maxListLength, Solver2.MaxListLen);
        }
    }
}
