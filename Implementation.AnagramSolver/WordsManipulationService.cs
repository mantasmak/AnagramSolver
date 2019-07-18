using Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.AnagramSolver
{
    public class WordsManipulationService : IWordsManipulator
    {
        private IWordRepository WordRepository { get; set; }

        private INumOfAllowedSearchesRepository NumOfSearchesRepository { get; set; }

        public WordsManipulationService(IWordRepository wordRepository, INumOfAllowedSearchesRepository numOfSearchesRepository)
        {
            WordRepository = wordRepository;
            NumOfSearchesRepository = numOfSearchesRepository;
        }

        public bool AddWord(string word, string userIp)
        {
            if(!WordRepository.WordExists(word))
            {
                WordRepository.Save(word);
                NumOfSearchesRepository.IncrementNumOfAllowedSearches(userIp);

                return true;
            }

            return false;
        }

        public bool RemoveWord(string word, string userIp)
        {
            if(WordRepository.WordExists(word))
            {
                WordRepository.Delete(word);
                NumOfSearchesRepository.DecrementNumOfAllowedSearches(userIp);

                return true;
            }

            return false;
        }

        public bool UpdateWord(string currentWord, string updatedWord, string userIp)
        {
            if (WordRepository.WordExists(currentWord))
            {
                WordRepository.Update(currentWord, updatedWord);
                NumOfSearchesRepository.IncrementNumOfAllowedSearches(userIp);

                return true;
            }

            return false;
        }
    }
}
