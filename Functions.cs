using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TD_Scrabble
{
    public static class Functions
    {

        public static int GetRandomInt(int min, int max)
        {
            var random = new Random();
            return random.Next(min, max);
        }

        public static void ClearConsole()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
        }

        public static IEnumerable<string> ReadFile(string path)
        {
            var lines = new Stack<string>();
            try
            {
                using var sr = new StreamReader(path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    lines.Push(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
                throw new IOException();
                
            }

            return lines.ToArray().Reverse();
        }

        public static void WriteFile(Stack<string> lines, string path)
        {
            try
            {
                using var sw = new StreamWriter(path);
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occured while trying to write file");
                Console.WriteLine(e.Message);
                throw new IOException();
            }
        }
        
    }
}