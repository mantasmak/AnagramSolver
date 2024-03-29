﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Contracts;

namespace Implementation.AnagramSolver
{
    public class SqlUserLogRepository : IUserLogRepository
    {
        private string connectionString;

        public int CountUserSearchesByIp(string userIp)
        {
            throw new NotImplementedException();
        }

        public SqlUserLogRepository()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public void Save(string ip, string word, DateTime time)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand insert = new SqlCommand("INSERT INTO dbo.UserLog (UserIp, Word, SearchTime) VALUES (@Ip, @Word, @Time)", connection);
                insert.Parameters.Add(new SqlParameter("Ip", ip));
                insert.Parameters.Add(new SqlParameter("Word", word));
                insert.Parameters.Add(new SqlParameter("Time", time));
                insert.ExecuteNonQuery();
                connection.Close();
            }
        }

        public IList<UserLogReport> GetUserLogReport()
        {
            bool added = false;
            List<UserLogReport> userLogReports = new List<UserLogReport>();
            List<string> anagrams = new List<string>();
            string query = "SELECT UserLog.UserIp, UserLog.SearchTime, UserLog.Word, Words.Word as Anagram " +
                "FROM((UserLog " +
                "INNER JOIN CachedWords ON UserLog.Word = CachedWords.Word) " +
                "INNER JOIN Words ON CachedWords.AnagramId = Words.Id)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectLogs = new SqlCommand(query, connection);
                using (SqlDataReader reader = selectLogs.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        added = false;
                        UserLogReport report = new UserLogReport();
                        report.UserIp = reader.GetString(0);
                        report.SearchTime = reader.GetDateTime(1);
                        report.Word = reader.GetString(2);
                        foreach(var logReport in userLogReports)
                        {
                            if(logReport.UserIp == report.UserIp && logReport.SearchTime == report.SearchTime)
                            {
                                logReport.Anagrams.Add(reader.GetString(3));
                                added = true;
                                break;
                            }
                        }
                        if (!added)
                        {
                            report.Anagrams = new List<string> { reader.GetString(3) };
                            userLogReports.Add(report);
                        }
                    }
                }
                connection.Close();
            }
            return userLogReports;
        }
    }
}
