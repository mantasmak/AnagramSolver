using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using Contracts;

namespace Implementation.AnagramSolver
{
    public class CacheRepository : ICacheRepository
    {
        private string connectionString;

        public CacheRepository()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public IList<string> GetCachedAnagrams(string word)
        {
            List<string> anagrams = new List<string>();
            List<int> anagramIds = new List<int>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCache = new SqlCommand("SELECT AnagramId FROM dbo.CachedWords WHERE Word=@Word", connection);
                selectCache.Parameters.Add(new SqlParameter("Word", word));
                SqlDataReader reader = selectCache.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        anagramIds.Add(reader.GetInt32(0));
                    }
                    reader.Close();

                    SqlCommand selectWords = new SqlCommand("SELECT Word FROM dbo.Words WHERE Id=@AnagramId", connection);
                    foreach (var id in anagramIds)
                    {
                        selectWords.Parameters.AddWithValue("AnagramId", id);
                        reader = selectWords.ExecuteReader();
                        while (reader.Read())
                        {
                            anagrams.Add(reader.GetString(0));
                        }

                        selectWords.Parameters.RemoveAt("AnagramId");
                        reader.Close();
                    }
                    return anagrams;
                }
                else
                {
                    return anagrams;
                }
            }
        }

        public void Save(string word, IEnumerable<string> anagrams)
        {
            List<int> anagramIds = new List<int>(); ;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand selectCache = new SqlCommand("SELECT AnagramId FROM dbo.CachedWords WHERE Word=@Word", connection);
                selectCache.Parameters.Add(new SqlParameter("Word", word));
                SqlDataReader reader = selectCache.ExecuteReader();
               
                if (!reader.HasRows)
                {
                    reader.Close();
                    SqlCommand selectId = new SqlCommand("SELECT Id FROM dbo.Words WHERE Word=@Anagram", connection);

                    foreach (var anagram in anagrams)
                    {
                        selectId.Parameters.AddWithValue("Anagram", anagram);
                        reader = selectId.ExecuteReader();
                        reader.Read();
                        anagramIds.Add(reader.GetInt32(0));
                        selectId.Parameters.RemoveAt("Word");
                        reader.Close();
                    }

                    SqlCommand insert = new SqlCommand("INSERT INTO dbo.CachedWords (Word, AnagramId) VALUES (@Word, @AnagramId)", connection);
                    insert.Parameters.Add(new SqlParameter("Word", word));
                    foreach (var anagramId in anagramIds)
                    {
                        insert.Parameters.AddWithValue("AnagramId", anagramId);
                        insert.ExecuteNonQuery();
                        insert.Parameters.RemoveAt("AnagramId");
                    }
                }
            }
        }
    }
}

