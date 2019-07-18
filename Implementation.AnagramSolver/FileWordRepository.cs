using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.IO;
using System.Linq;
using System.Configuration;

namespace Implementation.AnagramSolver
{
    public class FileWordRepository : IWordRepository
    {
        private string Path { get; set; }

        public FileWordRepository()
        {
            Path = ConfigurationManager.AppSettings["filePath"];
        }

        public FileWordRepository(string path)
        {
            Path = path;
        }

        public string Find(int wordId)
        {
            wordId--;
            List<Word> words = LoadWordsFromFile();

            return words[wordId].Name;
        }

        public IList<string> Find(string word)
        {
            List<Word> words = LoadWordsFromFile();
            var matches = words.Select(m => m.Name).Where(m => m.Contains(word)).ToList();

            return matches;
        }

        public IList<string> FindAnagrams(string word)
        {
            List<Word> words = LoadWordsFromFile();
            List<string> anagrams = new List<string>();
            string sortedSearchWord = string.Join(string.Empty, word.OrderBy(c => c));
            string sortedFileWord = string.Empty;

            foreach(var wordFromFile in words)
            {
                sortedFileWord = string.Join(string.Empty, wordFromFile.Name.OrderBy(c => c));
                if (string.Equals(sortedFileWord, sortedSearchWord) && !string.Equals(word, wordFromFile.Name))
                {
                    anagrams.Add(wordFromFile.Name);
                }
            }

            return anagrams;
        }

        public IList<string> GetAllWords()
        {
            var words = LoadWordsFromFile().Select(w => w.Name).ToList();

            return words;
        }

        private List<Word> LoadWordsFromFile()
        {
            HashSet<Word> words = new HashSet<Word>();

            if (File.Exists(Path))
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    string line = string.Empty;
                    string[] splitWords;

                    while ((line = sr.ReadLine()) != null)
                    {
                        splitWords = line.Split('\t');
                        words.Add(new Word(splitWords[0], splitWords[1]));
                    }
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

            return words.ToList<Word>();
        }

        public void Save(string word)
        {
            throw new NotImplementedException();
        }

        public bool WordExists(string word)
        {
            throw new NotImplementedException();
        }

        public void Delete(string word)
        {
            throw new NotImplementedException();
        }

        public void Update(string word)
        {
            throw new NotImplementedException();
        }

        public void Update(string currentWord, string updatedWord)
        {
            throw new NotImplementedException();
        }
    }
}
