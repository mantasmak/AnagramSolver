using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordService
    {
        IList<string> GetAllWords();
        IList<string> RecognizeWord(string word);
    }
}
