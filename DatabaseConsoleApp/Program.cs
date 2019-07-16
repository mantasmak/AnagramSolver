using System;
using System.Data;
using System.Data.SqlClient;
using Implementation.AnagramSolver;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using MainApp.EF.CodeFirst;
using MainApp.EF.DatabaseFirst;

namespace DatabaseConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool select = true;
            if (select)
            {
                EFCFCacheRepository efc = new EFCFCacheRepository();
                List<string> listc = efc.GetCachedAnagrams("dangus").ToList();
                foreach (var el in listc)
                {
                    Console.WriteLine(el);
                }
             /**
                using (var conn = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]))
                using (var command = new SqlCommand("dbo.TableDelete", conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    command.Parameters.Add(new SqlParameter("Table", "dbo.CachedWords"));
                    conn.Open();
                    command.ExecuteNonQuery();
                }
            **/
                //SelectWords();
            }
            else
            {
                FileWordReader fileWordReader = new FileWordReader();
                var wordsToInsert = fileWordReader.GetAllWords().ToList();

                try
                {
                    using (SqlConnection connection = new SqlConnection("Data Source=LT-LIT-SC-0009;" +
                        "Initial Catalog=MainAppDatabase;" +
                        "Integrated Security=True"))
                    {
                        string query = "INSERT INTO dbo.Words (Word) VALUES (@word)";
                        int result;
                        connection.Open();
                        foreach (string word in wordsToInsert)
                        {
                            using (SqlCommand insert = new SqlCommand(query, connection))
                            {
                                insert.Parameters.AddWithValue("@word", word);
                                result = insert.ExecuteNonQuery();

                                if (result < 0)
                                    Console.WriteLine($"Error inserting '{word}'");
                            }
                        }
                        connection.Close();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        static void SelectWords()
        {
            using (SqlConnection connection = new SqlConnection("Data Source=LT-LIT-SC-0009;" +
                    "Initial Catalog=MainAppDatabase;" +
                    "Integrated Security=True"))
            {
                connection.Open();
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words", connection);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetValue(0));
                        Console.WriteLine();
                    }
                }
                connection.Close();
            }

        }
    }
}
