using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

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
                Console.WriteLine(Properties.Strings.LanguageInputPrompt);
                string langSelect = Console.ReadLine();
                while(langSelect.Trim() != "1" && langSelect.Trim() != "2")
                {
                    Console.WriteLine(Properties.Strings.LanguageInputPrompt);
                    langSelect = Console.ReadLine();
                }
                if(langSelect.Trim() == "2") {
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-CA");
                }
                bool continueWordUnscrambling = true;

                while (continueWordUnscrambling)
                {
                    Console.WriteLine(Properties.Strings.InitInputPrompt);
                    String option = Console.ReadLine() ?? throw new Exception(Properties.Strings.NullString);

                    while (option.ToUpper() != "F" && option.ToUpper() != "M")
                    {
                        Console.WriteLine(Properties.Strings.UnrecognizedInputPrompt);
                        option = Console.ReadLine() ?? throw new Exception(Properties.Strings.NullString);
                    }

                    switch (option.ToUpper())
                    {
                        case "F":
                            Console.WriteLine(Properties.Strings.PathInputPrompt);
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case "M":
                            Console.WriteLine(Properties.Strings.ManualInputPrompt);
                            ExecuteScrambledWordsManualEntryScenario();
                            break;
                    }

                    Console.WriteLine(Properties.Strings.ContinuteInputPrompt);
                    string continueOption = Console.ReadLine();

                    while (continueOption.ToUpper() != "Y" && continueOption.ToUpper() != "N")
                    {
                        Console.WriteLine(Properties.Strings.InvalidInputPrompt);
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
                Console.WriteLine(Properties.Strings.Terminate + ex.Message);
            }
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            string filename = Console.ReadLine();
            while(_fileReader.Exists(filename) == false)
            {
                Console.WriteLine(Properties.Strings.InvalidFilePathInputPrompt);
                Console.WriteLine(Properties.Strings.PathInputPrompt);
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
                Console.Write(Properties.Strings.FoundMatch);
                Console.Write(word.ScrambledWord);
                Console.Write(Properties.Strings.FoundMatchWith);
                Console.WriteLine(word.Word);
            });
        }
    }
}
