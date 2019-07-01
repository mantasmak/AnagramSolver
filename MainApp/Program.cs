using System;
using System.IO;
using System.Collections.Generic;
//using Implementation.AnagramSolver;

namespace MainApp
{
    class Program
    {
         static public void Main(string[] args)
        {
            //AnagramSolver anagramSolver = new AnagramSolver();
            string abcd = new string("ABCD");
            Program p = new Program();
            p.FindPermutations(abcd);
        }

        private void FindPermutations(string word)
        {
            char[] wordToPermute = word.ToCharArray();
            //char[] permutedWord = 
                Permute(wordToPermute, 0, word.Length - 1);

            //return new string(permutedWord);
            
        }

        private void Permute(char[] word, int beg, int end)
        {
            if (beg == end)
            {
                Console.WriteLine(word);
                //return word;
            }
            else
            {
                for(int i = beg; i <= end; i++)
                {
                    word = Swap(word, beg, i);
                    Permute(word, beg + 1, end);
                    word = Swap(word, beg, i);
                }
            }

            //return null;
        }

        private char[] Swap(char[] arr, int a, int b)
        {
            if(a == b)
            {
                return arr;
            }

            char temp;
            temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
            return arr;
        }
    }
}
