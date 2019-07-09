using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    public interface IWordRepository
    {
        string Find(int wordId);

        IList<string> FindAnagrams(string word);

        Dictionary<string, List<string>> ReadWords();
    }
}
