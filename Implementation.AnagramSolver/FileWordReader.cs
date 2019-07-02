using System;
using System.Collections.Generic;
using System.Text;
using Interfaces.AnagramSolver;
using System.IO;
using System.Linq;

namespace Implementation.AnagramSolver
{
    class FileWordReader : IWordRepository
    {
        public Dictionary<string, List<string>> Words { get; set; }

        public FileWordReader()
        {
            Words = new Dictionary<string, List<string>>();
        }

        public void ReadWords()
        {
            string path = @"C:\Users\mantas\source\repos\MainApp\zodynas.txt";
            HashSet<Word> words = new HashSet<Word>();

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
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
                Console.WriteLine("File does not exist!");
            }

            ProcessWords(words);
        }

        private void ProcessWords(HashSet<Word> words)
        {
            string key;
            List<string> values;

            foreach(Word w in words)
            {
                key = string.Join(string.Empty, w.Name.OrderBy(c => c));
                if(!Words.TryGetValue(key, out values))
                {
                    Words.Add(key, new List<string>() { w.Name });
                }
                else
                {
                    Words[key].Add(w.Name);
                }
            }
        }

        public void PrintDictionary()
        {
            foreach (var d in Words.Take(1000))
            {
                Console.WriteLine($"{d.Key}");
                foreach (var s in d.Value)
                {
                    Console.WriteLine($"#################{s}");
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
