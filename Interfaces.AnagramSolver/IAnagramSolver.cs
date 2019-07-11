using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IAnagramSolver
    {
        IList<string> GetAnagrams(string words, string ip);
    }
}
