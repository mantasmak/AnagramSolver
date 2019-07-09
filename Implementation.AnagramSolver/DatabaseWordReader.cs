using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Interfaces.AnagramSolver;
using System.Linq;

namespace Implementation.AnagramSolver
{
    public class DatabaseWordReader : IWordRepository
    {
        private Dictionary<string, List<string>> Words { get; set; }
        private string connectionString;

        public DatabaseWordReader()
        {
            Words = new Dictionary<string, List<string>>();
            connectionString = "Data Source=LT-LIT-SC-0009;Initial Catalog=MainAppDatabase;Integrated Security=True";
        }

        public Dictionary<string, List<string>> ReadWords()
        {
            return Words;
        }

        public string Find(int wordId)
        {
            string searchedWord = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words WHERE Id=@Id", connection);
                select.Parameters.Add(new SqlParameter("Id", wordId));
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    reader.Read();
                    searchedWord = reader.GetString(0);
                }
                connection.Close();
            }
            return searchedWord;
        }

        public IList<string> FindAnagrams(string word)
        {
            int i = 0;
            List<string> anagrams = new List<string>();
            string sortedSearchWord = string.Join(string.Empty, word.OrderBy(c => c));
            string sortedDbWord = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words WHERE Word LIKE '@Test'", connection);
                //select.Parameters.Add(new SqlParameter("Test", $"%{word}%"));
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words", connection);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(i++);
                        sortedDbWord = string.Join(string.Empty, reader.GetString(0).OrderBy(c => c));
                        if (string.Equals(sortedDbWord, sortedSearchWord) && !string.Equals(word, reader.GetString(0)))
                        {
                            anagrams.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }

            return anagrams;
        }
    }
}
