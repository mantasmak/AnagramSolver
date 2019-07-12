using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICacheRepository
    {
        IList<string> GetCachedAnagrams(string word);
        void Save(string word, IEnumerable<string> anagrams);
    }
}
