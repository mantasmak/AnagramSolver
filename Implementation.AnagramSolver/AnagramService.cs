using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Contracts;
using System.Collections;
using System.Linq;
using System.Diagnostics;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Implementation.AnagramSolver
{
    public class AnagramService : IAnagramSolver
    {
        private readonly int allowedSearches = 2;

        private readonly AnagramServiceOptions Options;
        
        private IWordRepository Reader { get; set; }

        private ICacheRepository Cache { get; set; }

        private INumOfAllowedSearchesRepository AllowedSearches { get; set; }

        private IUserLogRepository UserLog { get; set; }

        public AnagramService(IWordRepository wordRepository, ICacheRepository cacheRepository, INumOfAllowedSearchesRepository allowedSearches, IOptionsMonitor<AnagramServiceOptions> options, IUserLogRepository userLog)
        {
            Reader = wordRepository;
            Cache = cacheRepository;
            AllowedSearches = allowedSearches;
            Options = options.CurrentValue;
            UserLog = userLog;
        }

        public IList<string> GetAnagrams(string word, string ip)
        {
            if(!AllowedSearches.CheckIfExists(ip))
            {
                AllowedSearches.SaveNewUser(ip, allowedSearches);
            }

            if (AllowedSearches.GetAmountOfSearches(ip) >= UserLog.CountUserSearchesByIp(ip))
            {
                var anagrams = Cache.GetCachedAnagrams(word);
                if (anagrams.Count == 0)
                {
                    anagrams = Reader.FindAnagrams(word);
                    Cache.Save(word, anagrams);
                }
                UserLog.Save(ip, word, DateTime.Now);
                return anagrams.Take(Options.MaxListLen).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
