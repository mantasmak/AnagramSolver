using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Interfaces.AnagramSolver;
using System.Collections;
using System.Linq;

namespace Implementation.AnagramSolver
{
    public class AnagramSolver : IAnagramSolver
    {
        public Dictionary<string, List<string>> Words { get; set; }

        public AnagramSolver()
        {
            Words = new Dictionary<string, List<string>>();
        }

        public Dictionary<string, IList<string>> FindAnagrams(List<string> words)
        {
            Dictionary<string, IList<string>> anagrams = new Dictionary<string, IList<string>>();

            foreach(string s in words)
            {
                anagrams.Add(s, GetAnagrams(s));
            }

            return anagrams;
        }

        public IList<string> GetAnagrams(string myWords)
        {
            FileWordReader reader = new FileWordReader();
            reader.ReadWords();
            List<string> anagrams;

            string key = string.Join(string.Empty, myWords.OrderBy(c => c));

            if (reader.Words.TryGetValue(key, out anagrams))
            {
                return anagrams;
            }
            else
            {
                return null;
            }
        }

    }
}
