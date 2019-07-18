using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordsManipulator
    {
        bool AddWord(string word, string userIp);
        bool RemoveWord(string word, string userIp);
        bool UpdateWord(string currentWord, string updatedWord, string userIp);
    }
}
