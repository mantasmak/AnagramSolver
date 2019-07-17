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

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly int allowedSearches = 2;

        public int MaxListLen { get; set; }
        
        private IWordRepository Reader { get; set; }

        private ICacheRepository Cache { get; set; }

        private INumOfAllowedSearchesRepository AllowedSearches { get; set; }

        private IConfiguration Configuration { get; set; }

        private IUserLogRepository UserLog { get; set; }

        public AnagramSolver(IWordRepository wordRepository, ICacheRepository cacheRepository, INumOfAllowedSearchesRepository allowedSearches, IConfiguration configuration, IUserLogRepository userLog)
        {
            Reader = wordRepository;
            Cache = cacheRepository;
            AllowedSearches = allowedSearches;
            Configuration = configuration;
            MaxListLen = Int32.Parse(Configuration["MaxListLen"]);
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
                return anagrams.Take(MaxListLen).ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
