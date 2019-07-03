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
        private int MaxListLen { get; set; }

        public AnagramSolver()
        {
            MaxListLen = 10;
        }
        public AnagramSolver(int maxListLen)
        {
            MaxListLen = maxListLen;
        }

        public IList<string> GetAnagram(string myWords)
        {
            FileWordReader reader = new FileWordReader();
            reader.ReadWords();
            List<string> anagrams;
            int indexToRemove = -1;

            string key = string.Join(string.Empty, myWords.OrderBy(c => c));

            if (reader.Words.TryGetValue(key, out anagrams))
            {
                indexToRemove = anagrams.FindIndex(a => a == myWords);
                if (indexToRemove != -1)
                    anagrams.RemoveAt(indexToRemove);

                return anagrams.Take(MaxListLen).ToList();
            }
            else
            {
                return null;
            }
        }

    }
}
