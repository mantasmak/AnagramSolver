using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;

namespace MainApp.EF.DatabaseFirst
{
    public class EFDBFWordReader : IWordRepository
    {
        public string Find(int wordId)
        {
            string word;
            using(var context = new MainAppDatabaseContext())
            {
                var query = context.Words.Where(w => w.Id == wordId).First();
                word = query.Word;
            }
            return word;
        }

        public IList<string> Find(string word)
        {
            List<string> words = new List<string>();
            using(var context = new MainAppDatabaseContext())
            {
                var query = context.Words.Select(w => w.Word).Where(w => w.Contains(word));
                words.AddRange(query);
            }
            return words;
        }

        public IList<string> FindAnagrams(string word)
        {
            List<string> anagrams = new List<string>();
            using(var context = new MainAppDatabaseContext())
            {
                var query = context.Words.Select(w => w.Word).Where(w => string.Join(string.Empty, word.OrderBy(a => a)) == string.Join(string.Empty, w.OrderBy(c => c)) && word != w);
                anagrams.AddRange(query);
            }
            return anagrams;
        }

        public IList<string> GetAllWords()
        {
            List<string> words = new List<string>();
            using(var context = new MainAppDatabaseContext())
            {
                var query = context.Words.Select(w => w.Word);
                words.AddRange(query);
            }
            return words;
        }
    }
}
