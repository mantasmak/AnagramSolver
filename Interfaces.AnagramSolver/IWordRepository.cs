using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordRepository
    {
        string Find(int wordId);

        IList<string> Find(string word);

        IList<string> FindAnagrams(string word);

        IList<string> GetAllWords();
    }
}
