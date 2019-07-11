using System;
using System.IO;
using System.Collections.Generic;
using Implementation.AnagramSolver;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;

namespace MainApp
{
    class Program
    {
        static public void Main(string[] args)
        {
            /**
            Console.OutputEncoding = Encoding.UTF8;
            var conf = ConfigurationManager.AppSettings;
            int minWordLen = Int32.Parse(conf["minWordLen"]);
            var anSo = new AnagramSolver();
            IList<string> anagrams;

            while (true)
            {
                string word = Console.ReadLine();

                if(word.Length < minWordLen)
                {
                    Console.WriteLine("Žodis per trumpas.");
                    continue;
                }

                anagrams = anSo.GetAnagrams(word);
                
                if (anagrams != null && anagrams.Any())
                {
                    foreach (var anagram in anagrams)
                    {
                        Console.WriteLine($"\t{anagram}");
                    }
                }
                else
                {
                    Console.WriteLine($"\tAnagramai nerasti.");
                }
            }
    **/
        }
    }
}
