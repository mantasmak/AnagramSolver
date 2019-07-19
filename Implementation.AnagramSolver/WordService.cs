using System;
using System.Collections.Generic;
using System.Text;
using Contracts;

namespace Implementation.AnagramSolver
{
    public class WordService : IWordService
    {
        IWordRepository wordRepository;

        public WordService(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

        public IList<string> GetAllWords()
        {
            return wordRepository.GetAllWords();
        }

        public IList<string> RecognizeWord(string word)
        {
            return wordRepository.Find(word);
        }
    }
}
