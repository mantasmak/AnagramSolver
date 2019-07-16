using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;

namespace MainApp.EF.CodeFirst
{
    public class EFCFNumOfAllowedSearchesRepository : INumOfAllowedSearchesRepository
    {
        public bool CheckIfExists(string userIp)
        {
            using(MainAppDatabaseContext context = new MainAppDatabaseContext())
            {
                var selectIp = context.NumOfAllowedSearches.Where(u => u.UserIp == userIp);
                return selectIp.Any() ? true : false;
            }
        }

        public int GetAmountOfSearches(string userIp)
        {
            using(MainAppDatabaseContext context = new MainAppDatabaseContext())
            {
                var selectAmount = context.NumOfAllowedSearches.Where(u => u.UserIp == userIp).Select(a => a.Amount);
                return selectAmount.Single();
            }
        }

        public void SaveNewUser(string userIp, int amount)
        {
            using(MainAppDatabaseContext context = new MainAppDatabaseContext())
            {
                NumOfAllowedSearchesEntity allowedSearches = new NumOfAllowedSearchesEntity();
                allowedSearches.UserIp = userIp;
                allowedSearches.Amount = amount;

                context.NumOfAllowedSearches.Add(allowedSearches);
                context.SaveChanges();
            }
        }
    }
}
