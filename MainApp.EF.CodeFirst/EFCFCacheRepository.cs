using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Contracts;

namespace MainApp.EF.CodeFirst
{
    public class EFCFCacheRepository : ICacheRepository
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
            using (var context = new MainAppDatabaseContext())
            {
                var wordId = context.Words.Where(w => anagrams.Contains(w.Word)).Select(i => i.Id);
                foreach (var id in wordId)
                {
                    CachedWords cache = new CachedWords();
                    cache.Word = word;
                    cache.AnagramId = id;
                    context.CachedWords.Add(cache);
                }
                context.SaveChanges();
            }
        }
    }
}
