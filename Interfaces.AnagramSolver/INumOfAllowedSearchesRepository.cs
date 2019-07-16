using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface INumOfAllowedSearchesRepository
    {
        int GetAmountOfSearches(string userIp);
        bool CheckIfExists(string userIp);
        void SaveNewUser(string userIp, int amount);
    }
}
