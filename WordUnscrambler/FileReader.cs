using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WordUnscrambler
{
    class FileReader
    {
        public int Count(string filename)
        {
            int lines = 0;
            try
            {
                using (StreamReader readFile = new StreamReader(filename))
                {
                    string line;
                    while ((line = readFile.ReadLine()) != null)
                    {
                        lines++;
                    }
                    readFile.ReadToEnd();
                }
            }catch(FileNotFoundException exception)
            {
                Console.WriteLine("File was not found.. " + exception.Message);
            }
            return lines;
        }
        public string[] Read(string filename)
        {
            string[] fileLines = new string[Count(filename)];
            try
            {
                using (StreamReader readFile = new StreamReader(filename))
                {
                    for(int i = 0; i < fileLines.Length; i++)
                    {
                        string line = readFile.ReadLine();
                        fileLines[i] = line;
                    }
                    readFile.ReadToEnd();
                }
            }catch(FileNotFoundException exception)
            {
                Console.WriteLine("File was not found.. " + exception.Message);
            }
            return fileLines;
        }
        public bool Exists(string filename)
        {
            return File.Exists(filename); // Why I'm doing this is because I have IO imported here, don't want to import it again in the Main
        }
    }
}
