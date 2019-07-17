using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordDeleter
    {
        bool RemoveWord(string word, string userIp);
    }
}
