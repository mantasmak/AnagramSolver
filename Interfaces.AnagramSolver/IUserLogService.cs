using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IUserLogService
    {
        List<UserLogReport> GenerateUserLogReport();
    }
}
