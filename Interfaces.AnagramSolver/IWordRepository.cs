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

        void Save(string word);

        void Delete(string word);

        void Update(string currentWord, string updatedWord);

        bool WordExists(string word);
    }
}
