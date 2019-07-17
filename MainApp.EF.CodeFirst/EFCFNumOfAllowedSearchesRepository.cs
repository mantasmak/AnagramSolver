using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MainApp.EF.CodeFirst
{
    public class EFCFNumOfAllowedSearchesRepository : INumOfAllowedSearchesRepository
    {
        MainAppDatabaseContext context;

        public EFCFNumOfAllowedSearchesRepository(MainAppDatabaseContext context)
        {
            this.context = context;
        }

        public bool CheckIfExists(string userIp)
        {
            var selectIp = context.NumOfAllowedSearches.Where(u => u.UserIp == userIp);
            return selectIp.Any() ? true : false;

        }

        public int GetAmountOfSearches(string userIp)
        {
            var selectAmount = context.NumOfAllowedSearches.Where(u => u.UserIp == userIp).Select(a => a.Amount);
            return selectAmount.Single();

        }

        public void SaveNewUser(string userIp, int amount)
        {
            NumOfAllowedSearchesEntity allowedSearches = new NumOfAllowedSearchesEntity();
            allowedSearches.UserIp = userIp;
            allowedSearches.Amount = amount;

            context.NumOfAllowedSearches.Add(allowedSearches);
            context.SaveChanges();

        }

        public bool IncrementNumOfAllowedSearches(string ip)
        {
            var user = context.NumOfAllowedSearches.FirstOrDefault(u => u.UserIp == ip);

            if (user != null)
            {
                user.Amount++;

                context.NumOfAllowedSearches.Update(user);
                context.SaveChanges();

                return true;
            }

            return false;

        }

        public bool DecrementNumOfAllowedSearches(string ip)
        {
            var user = context.NumOfAllowedSearches.FirstOrDefault(u => u.UserIp == ip);

            if (user != null)
            {
                user.Amount--;

                context.NumOfAllowedSearches.Update(user);
                context.SaveChanges();

                return true;
            }

            return false;

        }
    }
}
