using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.AnagramSolver
{
    interface IAnagramSolver
    {
        IList<string> GetAnagrams(string myWords);
    }
}
