using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class UserLogService : IUserLogService
    {
        IUserLogRepository userLogRepository;

        public UserLogService(IUserLogRepository userLogRepository)
        {
            this.userLogRepository = userLogRepository;
        }

        public List<UserLogReport> GenerateUserLogReport()
        {
            bool added = false;
            List<UserLogReport> userLogReports = new List<UserLogReport>();
            var tempUserLogReport = userLogRepository.GetUserLogReport();

            foreach (var tempUserLog in tempUserLogReport)
            {
                added = false;
                UserLogReport reportToAdd = new UserLogReport();
                reportToAdd.UserIp = tempUserLog.UserIp;
                reportToAdd.SearchTime = tempUserLog.SearchTime;
                reportToAdd.Word = tempUserLog.Word;
                foreach (var logReport in userLogReports)
                {
                    if (logReport.UserIp == reportToAdd.UserIp && logReport.SearchTime == reportToAdd.SearchTime)
                    {
                        logReport.Anagrams.Add(tempUserLog.Anagrams[0]);
                        added = true;
                        break;
                    }
                }
                if (!added)
                {
                    reportToAdd.Anagrams = tempUserLog.Anagrams;
                    userLogReports.Add(reportToAdd);
                }
            }

            return userLogReports;
        }
    }
}
