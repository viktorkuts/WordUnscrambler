using System;
using System.Collections.Generic;
using System.Linq;

namespace WordUnscrambler
{
    class WordMatcher
    {
        public List<MatchedWord> Match(string[] scrambledWords, string[] wordList)
        {
            List<MatchedWord> matchedWords = new List<MatchedWord>();

            foreach (var scrambledWord in scrambledWords)
            {
                // Convert the scrambled word to a sorted character array.
                var scrambledWordSorted = String.Concat(scrambledWord.OrderBy(c => c));

                foreach (var word in wordList)
                {
                    // Convert the word to a sorted character array.
                    var wordSorted = String.Concat(word.OrderBy(c => c));

                    if (scrambledWordSorted.Equals(wordSorted))
                    {
                        matchedWords.Add(BuildMatchedWord(scrambledWord, word));
                        break;  // Break if match was successfully found.
                    }
                }
            }

            MatchedWord BuildMatchedWord(string scrambledWord, string word)
            {
                return new MatchedWord
                {
                    ScrambledWord = scrambledWord,
                    Word = word
                };
            }

            return matchedWords;
        }
    }

}
