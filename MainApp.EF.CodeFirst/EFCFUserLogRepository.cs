using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;

namespace MainApp.EF.CodeFirst
{
    public class EFCFUserLogRepository : IUserLogRepository
    {
        public int CountUserSearchesByIp(string userIp)
        {
            using(MainAppDatabaseContext context = new MainAppDatabaseContext())
            {
                var selectUserCount = context.UserLog.Where(u => u.UserIp == userIp).Count();

                return selectUserCount;
            }
        }

        public List<UserLogReport> GetUserLogReport()
        {
            bool added = false;
            List<UserLogReport> userLogReports = new List<UserLogReport>();
            using (var context = new MainAppDatabaseContext())
            {
                var tempUserLogReport = context.UserLog.Join(context.CachedWords,
                                                         log => log.Word,
                                                         cache => cache.Word,
                                                         (log, cache) => new { log, cache })
                                                   .Join(context.Words,
                                                         o => o.cache.AnagramId,
                                                         w => w.Id,
                                                         (o, w) => new { o, w })
                                                   .Select(r => new UserLogReport
                                                   {
                                                       UserIp = r.o.log.UserIp,
                                                       SearchTime = r.o.log.SearchTime,
                                                       Word = r.o.log.Word,
                                                       Anagrams = new List<string>() { r.w.Word }
                                                   });
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
            }

            return userLogReports;
        }

        public void Save(string ip, string word, DateTime time)
        {
            UserLogEntity log = new UserLogEntity();
            log.UserIp = ip;
            log.Word = word;
            log.SearchTime = time;

            using (var context = new MainAppDatabaseContext())
            {
                context.UserLog.Add(log);
                context.SaveChanges();
            }
        }
    }
}