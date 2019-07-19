using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MainApp.EF.CodeFirst
{
    public class EFCFUserLogRepository : IUserLogRepository
    {
        MainAppDatabaseContext context;

        public EFCFUserLogRepository(MainAppDatabaseContext context)
        {
            this.context = context;
        }

        public int CountUserSearchesByIp(string userIp)
        {
            var selectUserCount = context.UserLog.Where(u => u.UserIp == userIp).Count();

            return selectUserCount;
        }

        public IList<UserLogReport> GetUserLogReport()
        {
            List<UserLogReport> userLogReports = new List<UserLogReport>();
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

            return tempUserLogReport.ToList();
        }

        public void Save(string ip, string word, DateTime time)
        {
            UserLogEntity log = new UserLogEntity();
            log.UserIp = ip;
            log.Word = word;
            log.SearchTime = time;

            context.UserLog.Add(log);
            context.SaveChanges();

        }
    }
}