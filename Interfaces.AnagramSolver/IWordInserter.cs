using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordInserter
    {
        bool AddWord(string word, string userIp);
    }
}
