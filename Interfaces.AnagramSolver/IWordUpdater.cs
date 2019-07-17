using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordUpdater
    {
        bool UpdateWord(string currentWord, string updatedWord, string userIp);
    }
}
