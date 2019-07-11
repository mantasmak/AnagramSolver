using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUserLogRepository
    {
        void Save(string ip, string word, DateTime time);
        List<UserLogReport> GetUserLogReport();
    }
}
