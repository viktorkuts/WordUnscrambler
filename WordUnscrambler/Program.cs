using System;
using System.Collections.Generic;
using System.Linq;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {
            try
            {
                bool continueWordUnscrambling = true;

                while (continueWordUnscrambling)
                {
                    Console.WriteLine("Enter scrambled word(s) manually or as a file: F - file / M - manual");
                    String option = Console.ReadLine() ?? throw new Exception("String is empty");

                    while (option.ToUpper() != "F" && option.ToUpper() != "M")
                    {
                        Console.WriteLine("The entered option was not recognized. Please enter F for file or M for manual entry.");
                        option = Console.ReadLine() ?? throw new Exception("String is empty");
                    }

                    switch (option.ToUpper())
                    {
                        case "F":
                            Console.WriteLine("Enter full path including the file name: ");
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case "M":
                            Console.WriteLine("Enter word(s) manually (separated by commas if multiple): ");
                            ExecuteScrambledWordsManualEntryScenario();
                            break;
                    }

                    Console.WriteLine("Would you like to continue? Y/N");
                    string continueOption = Console.ReadLine();

                    while (continueOption.ToUpper() != "Y" && continueOption.ToUpper() != "N")
                    {
                        Console.WriteLine("Invalid input. Please enter Y to continue or N to exit.");
                        continueOption = Console.ReadLine();
                    }

                    if (continueOption.ToUpper() == "N")
                    {
                        continueWordUnscrambling = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The program will be terminated." + ex.Message);
            }
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            string filename = Console.ReadLine();
            while(_fileReader.Exists(filename) == false)
            {
                Console.WriteLine("File does not exist..\nEnter full path including the file name: ");
                filename = Console.ReadLine();
            }
            string[] scrambledWords = _fileReader.Read(filename);
            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            // Read user input.
            string input = Console.ReadLine();

            // Split words based on commas. The trim method makes sure there's no spaces around the entered words.
            string[] scrambledWords = input.Split(',').Select(word => word.Trim()).ToArray();

            DisplayMatchedUnscrambledWords(scrambledWords);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            //read the list of words from the system file. 
            string[] wordList = _fileReader.Read("wordlist.txt");

            //call a word matcher method to get a list of structs of matched words.
            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);
            matchedWords.ForEach(word =>
            {
                Console.WriteLine("Found match " + word.ScrambledWord + " with " + word.Word);
            });
        }
    }
}
