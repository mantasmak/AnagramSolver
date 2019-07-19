using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Contracts;
using System.Linq;
using System.Configuration;
using System.Collections.Specialized;

namespace Implementation.AnagramSolver
{
    public class SqlWordRepository : IWordRepository
    {
        private string connectionString;

        public SqlWordRepository()
        {
            connectionString = ConfigurationManager.AppSettings["connectionString"];
        }

        public void Delete(string word)
        {
            throw new NotImplementedException();
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
        
        public IList<string> Find(string word)
        {
            List<string> searchedWords = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words WHERE Word LIKE @Word", connection);
                select.Parameters.Add(new SqlParameter("Word", $"%{word}%"));
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        searchedWords.Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }
            return searchedWords;
        }

        public IList<string> FindAnagrams(string word)
        {
            List<string> anagrams = new List<string>();
            string sortedSearchWord = string.Join(string.Empty, word.OrderBy(c => c));
            string sortedDbWord = string.Empty;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words", connection);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sortedDbWord = string.Join(string.Empty, reader.GetString(0).OrderBy(c => c));
                        if (string.Equals(sortedDbWord, sortedSearchWord) && !string.Equals(word, reader.GetString(0)))
                        {
                            anagrams.Add(reader.GetString(0));
                        }
                    }
                }
                connection.Close();
            }
            foreach (var an in anagrams)
                Console.WriteLine(an);

            return anagrams;
        }

        public IList<string> GetAllWords()
        {
            List<string> searchedWords = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand select = new SqlCommand("SELECT Word FROM dbo.Words", connection);
                using (SqlDataReader reader = select.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        searchedWords.Add(reader.GetString(0));
                    }
                }
                connection.Close();
            }
            return searchedWords;
        }

        public void Save(string word)
        {
            throw new NotImplementedException();
        }

        public void Update(string currentWord, string updatedWord)
        {
            throw new NotImplementedException();
        }

        public bool WordExists(string word)
        {
            throw new NotImplementedException();
        }
    }
}
