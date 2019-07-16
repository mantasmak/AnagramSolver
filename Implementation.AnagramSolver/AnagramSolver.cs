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
        private readonly int allowedSearches = 2;

        public int MaxListLen { get; set; }
        
        private IWordRepository Reader { get; set; }

        private ICacheRepository Cache { get; set; }

        private INumOfAllowedSearchesRepository AllowedSearches { get; set; }

        public AnagramSolver(IWordRepository wordRepository, ICacheRepository cacheRepository, INumOfAllowedSearchesRepository allowedSearches)
        {
            MaxListLen = Int32.Parse(ConfigurationManager.AppSettings["maxListLen"]);
            Reader = wordRepository;
            Cache = cacheRepository;
            AllowedSearches = allowedSearches;
        }

        public IList<string> GetAnagrams(string word, string ip)
        {
            if(!AllowedSearches.CheckIfExists(ip))
            {
                AllowedSearches.SaveNewUser(ip, allowedSearches);
            }

            if (AllowedSearches.GetAmountOfSearches(ip) <= allowedSearches)
            {
                var anagrams = Cache.GetCachedAnagrams(word);
                if (anagrams.Count == 0)
                {
                    anagrams = Reader.FindAnagrams(word);
                    Cache.Save(word, anagrams);
                }
                UserLogRepository userLog = new UserLogRepository();
                userLog.Save(ip, word, DateTime.Now);
                return anagrams;
            }
            else
            {
                return null;
            }
        }
    }
}
