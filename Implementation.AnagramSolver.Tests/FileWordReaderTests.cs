using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace Implementation.AnagramSolver.Tests
{
    [TestFixture]
    class FileWordReaderTests
    {
        [Test]
        public void LettersAreInAlphabeticalOrder()
        {
            //var conf = ConfigurationManager.AppSettings;
            FileWordReader reader = new FileWordReader(@"C:\Users\mantas\source\repos\MainApp\Implementation.AnagramSolver.Tests\zodynasTest.txt");

            reader.ReadWords();

            Assert.IsTrue(reader.Words.ContainsKey("alsu"));
            Assert.IsTrue(reader.Words.ContainsKey("adgnsu"));
        }
    }
}
