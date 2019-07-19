using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class FileWordReaderTests
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
        public void DictionaryKeyLettersAreInAlphabeticalOrder()
        {
            FileWordReader reader = new FileWordReader(Path);

            Assert.IsTrue(reader.ReadWords().ContainsKey("alsu"));
            Assert.IsTrue(reader.ReadWords().ContainsKey("adgnsu"));
        }

        [Test]
        public void ThrowExceptionIfFileDoesNotExist()
        {
            string notExistingFilePath = Config["notExistingFilePath"];

            Assert.Throws<FileNotFoundException>(() => new FileWordReader(notExistingFilePath));
        }

        [Test]
        public void AddsCorrectWordsToDictionaryValues()
        {
            FileWordReader reader = new FileWordReader(Path);
            var dictWords = reader.ReadWords();
            List<string> words;
            bool wordExists = dictWords.TryGetValue("adgnsu", out words);

            Assert.IsTrue(wordExists);
            Assert.IsTrue(words.Contains("dangus"));
            Assert.IsTrue(words.Contains("dugnas"));
            Assert.IsTrue(words.Contains("gandus"));
        }
    }
}
