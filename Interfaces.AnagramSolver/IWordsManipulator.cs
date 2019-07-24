using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IWordsManipulator
    {
        bool AddWord(string word, string userIp);
        bool RemoveWord(string word, string userIp);
        bool RemoveWord(int id, string userIp);
        bool UpdateWord(string currentWord, string updatedWord, string userIp);
        bool UpdateWord(int id, string updatedWord, string userIp);
    }
}
