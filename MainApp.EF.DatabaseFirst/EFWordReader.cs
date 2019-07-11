using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace MainApp.EF.DatabaseFirst
{
    class EFWordReader : IWordRepository
    {
        public string Find(int wordId)
        {
            throw new NotImplementedException();
        }

        public IList<string> Find(string word)
        {
            throw new NotImplementedException();
        }

        public IList<string> FindAnagrams(string word)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetAllWords()
        {
            throw new NotImplementedException();
        }
    }
}
