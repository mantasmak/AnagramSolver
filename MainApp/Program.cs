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
            Console.OutputEncoding = Encoding.UTF8;
            var conf = ConfigurationManager.AppSettings;
            List<string> words = new List<string>();

            foreach (var c in conf.AllKeys)
            {
                words.Add(conf.Get(c));
            }

            var anSo = new AnagramSolver();
            var anagrams = anSo.FindAnagrams(words);
            if (anagrams == null)
            {
                Console.WriteLine("Anagramai nerasti");
                return;
            }

            foreach (var d in anagrams)
            {
                if (d.Value != null)
                {
                    foreach (var s in d.Value)
                    {
                        Console.WriteLine($"{s}");
                    }
                }
            }
        }
    }
}
