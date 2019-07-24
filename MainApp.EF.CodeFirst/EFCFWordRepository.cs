using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MainApp.EF.CodeFirst
{
    public class EFCFWordRepository : IWordRepository
    {
        MainAppDatabaseContext context;

        public EFCFWordRepository(MainAppDatabaseContext context)
        {
            this.context = context;
        }

        public void Delete(string word)
        {
            var wordToDelete = context.Words.FirstOrDefault(w => w.Word == word);

            context.Words.Remove(wordToDelete);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var wordToDelete = context.Words.FirstOrDefault(w => w.Id == id);

            context.Words.Remove(wordToDelete);
            context.SaveChanges();
        }

        public string Find(int wordId)
        {
            string word;
            var query = context.Words.Where(w => w.Id == wordId).First();
            word = query.Word;

            return word;
        }

        public IList<string> Find(string word)
        {
            List<string> words = new List<string>();
            var query = context.Words.Select(w => w.Word).Where(w => w.Contains(word));
            words.AddRange(query);

            return words;
        }

        public IList<string> FindAnagrams(string word)
        {
            List<string> anagrams = new List<string>();
            var query = context.Words.Select(w => w.Word).Where(w => string.Join(string.Empty, word.OrderBy(a => a)) == string.Join(string.Empty, w.OrderBy(c => c)) && word != w);
            anagrams.AddRange(query);

            return anagrams;
        }

        public IList<string> GetAllWords()
        {
            List<string> words = new List<string>();
            var query = context.Words.Select(w => w.Word);
            words.AddRange(query);

            return words;
        }

        public void Save(string word)
        {
            WordsEntity newWord = new WordsEntity();
            newWord.Word = word;
            context.Words.Add(newWord);
            context.SaveChanges();
        }

        public void Update(string currentWord, string updatedWord)
        {
            var wordToUpdate = context.Words.FirstOrDefault(w => w.Word == currentWord);

            wordToUpdate.Word = updatedWord;

            context.Words.Update(wordToUpdate);
            context.SaveChanges();
        }

        public void Update(int id, string word)
        {
            var wordToUpdate = context.Words.FirstOrDefault(w => w.Id == id);

            wordToUpdate.Word = word;

            context.Words.Update(wordToUpdate);
            context.SaveChanges();
        }

        public bool WordExists(string word)
        {
            var selectWord = context.Words.Where(w => w.Word == word);

            if (selectWord.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WordExists(int id)
        {
            var selectWord = context.Words.Where(w => w.Id == id);

            if (selectWord.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
