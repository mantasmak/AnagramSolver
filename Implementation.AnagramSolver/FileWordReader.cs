using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.AnagramSolver;
using System.IO;
using System.Linq;

namespace Implementation.AnagramSolver
{
    public class FileWordReader : IWordRepository
    {
        private Dictionary<string, List<string>> Words { get; set; }
        private string Path { get; set; }

        public FileWordReader()
        {
            Words = new Dictionary<string, List<string>>();
            Path = @"C:\Users\mantas\source\repos\MainApp\zodynas.txt";
            try
            {
                LoadWordsFromFile();
            }
            catch(FileNotFoundException e)
            {
                throw e;
            }
        }

        public FileWordReader(string path)
        {
            Words = new Dictionary<string, List<string>>();
            Path = path;
            try
            {
                LoadWordsFromFile();
            }
            catch (FileNotFoundException e)
            {
                throw e;
            }
        }

        public Dictionary<string, List<string>> ReadWords()
        {
            return Words;
        }

        public string Find(int wordId)
        {
            wordId--;
            List<Word> words = LoadWordsFromFile();

            return words[wordId].Name;
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
                if(string.Equals(sortedFileWord, sortedSearchWord))
                {
                    anagrams.Add(wordFromFile.Name);
                }
            }

            return anagrams;
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

        private void ProcessWords(HashSet<Word> words)
        {
            string key;
            List<string> values;

            foreach(Word word in words)
            {
                key = string.Join(string.Empty, word.Name.OrderBy(c => c));
                if(!Words.TryGetValue(key, out values))
                {
                    Words.Add(key, new List<string>() { word.Name });
                }
                else
                {
                    Words[key].Add(word.Name);
                }
            }
        }

        public void PrintDictionary()
        {
            foreach (var word in Words.Take(1000))
            {
                Console.WriteLine($"{word.Key}");
                foreach (var value in word.Value)
                {
                    Console.WriteLine($"\t{value}");
                }
            }
        }
        /**
        private void LoadWordsToDictionary(HashSet<Word> words)
        {
            var groupedWords = words.GroupBy(
                w => w.Type,
                w => w.Name,
                (key, value) => new { Type = key, Words = value.ToList() });

            foreach (var word in groupedWords)
            {
                Words.Add(word.Type, word.Words);
            }
        }
    **/
    }
}
