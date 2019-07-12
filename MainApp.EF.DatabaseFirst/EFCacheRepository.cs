using Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MainApp.EF.DatabaseFirst
{
    public class EFCacheRepository : ICacheRepository
    {
        public IList<string> GetCachedAnagrams(string word)
        {
            List<string> anagrams = new List<string>();
            using (var context = new MainAppDatabaseContext())
            {
                var query = context.CachedWords.Where(c => c.Word == word).Join(context.Words,
                                                            cache => cache.AnagramId,
                                                            w => w.Id,
                                                            (cache, w) => w.Word);
                anagrams.AddRange(query);
            }
            return anagrams;
        }

        public void Save(string word, IEnumerable<string> anagrams)
        {
            CachedWords cache = new CachedWords();
            cache.Word = word;
            
        }
    }
}
