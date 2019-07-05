using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebAppTester
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string word = Console.ReadLine();
                var test = SendRequest(word);
                List<string> anagrams = JsonConvert.DeserializeObject<List<string>>(test.Result);
                foreach (var anagram in anagrams)
                {
                    Console.WriteLine(anagram);
                }
                Console.WriteLine();
            }
        }

        static async Task<string> SendRequest(string word)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://localhost:44393/Data/GetAnagrams?word={word}");
            return response;
        }
    }
}
