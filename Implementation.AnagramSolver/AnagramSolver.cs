using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Contracts;
using System.Collections;
using System.Linq;
using System.Diagnostics;
using System.Configuration;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        public int MaxListLen { get; set; }
        
        private IWordRepository Reader { get; set; }

        private ICacheRepository Cache { get; set; }

        public AnagramSolver(IWordRepository wordRepository)
        {
            MaxListLen = Int32.Parse(ConfigurationManager.AppSettings["maxListLen"]);
            Reader = wordRepository;
            Cache = new CacheRepository();
        }

        public IList<string> GetAnagrams(string word, string ip)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            var anagrams = Cache.GetCachedAnagrams(word);
            if(anagrams.Count == 0)
            {
                anagrams = Reader.FindAnagrams(word);
                Cache.Save(word, anagrams);
            }
            stopWatch.Stop();
            UserLogRepository userLog = new UserLogRepository();
            userLog.Save(ip, word, DateTime.Now);

            return anagrams;
        }
    }
}
