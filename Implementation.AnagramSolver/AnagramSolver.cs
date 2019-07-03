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
        private FileWordReader Reader { get; set; }

        public AnagramSolver()
        {
            MaxListLen = 10;
            Reader = new FileWordReader();
        }
        public AnagramSolver(int maxListLen)
        {
            MaxListLen = maxListLen;
            Reader = new FileWordReader();
        }

        public IList<string> GetAnagrams(string myWords)
        {
            List<string> anagrams;
            int indexToRemove = -1;

            string key = string.Join(string.Empty, myWords.OrderBy(c => c));

            if (Reader.Words.TryGetValue(key, out anagrams))
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
