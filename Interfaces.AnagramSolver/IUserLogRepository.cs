using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts
{
    public interface IUserLogRepository
    {
        void Save(string ip, string word, DateTime time);
        IList<UserLogReport> GetUserLogReport();
        int CountUserSearchesByIp(string userIp);
    }
}
