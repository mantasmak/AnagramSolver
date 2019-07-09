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
            LoadWordsFromFile();
        }

        public Dictionary<string, List<string>> ReadWords()
        {
            return Words;
        }

        private void LoadWordsFromFile()
        {
            HashSet<string> words = new HashSet<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words", connection);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        words.Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }

            ProcessWords(words);
        }

        private void ProcessWords(HashSet<string> words)
        {
            string key;
            List<string> values;

            foreach (string word in words)
            {
                key = string.Join(string.Empty, word.OrderBy(c => c));
                if (!Words.TryGetValue(key, out values))
                {
                    Words.Add(key, new List<string>() { word });
                }
                else
                {
                    Words[key].Add(word);
                }
            }
        }

        public void PrintDictionary()
        {
            foreach (var word in Words)
            {
                Console.WriteLine($"{word.Key}");
                foreach (var value in word.Value)
                {
                    Console.WriteLine($"\t{value}");
                }
            }
        }
    }
}
