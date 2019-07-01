using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Implementation.AnagramSolver
{
    class AnagramSolver
    {
        private HashSet<string> Words { get; set; }

        public AnagramSolver()
        {
            Words = new HashSet<string>();
        }

        private void ReadWords()
        {
            string path = @"C:\Users\mantas\source\repos\MainApp\zodynas.txt";

            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line = "";
                    string[] splitWords;

                    while ((line = sr.ReadLine()) != null)
                    {
                        splitWords = line.Split('\t');
                        Words.Add(splitWords[0]);
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist!");
            }
        }


    }
}
