using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class PlayerMenu : Menu
    {
        
        public PlayerMenu(Joueur player, List<Option> options)
        {
            Options = options;
            Description = $"Bonjour {player.Name}, c'est à vous de jouer !";
        }

        public void WriteMenu(Jeu jeu, Joueur player)
        {
            Functions.ClearConsole();
            jeu.DisplayBoard();
            player.DisplayPlayerInfo();
            Console.WriteLine(Description + "\n");
            
            foreach (var option in Options)
            {
                Console.Write(Options.IndexOf(option) == Index ? "> " : " ");

                Console.WriteLine(option.Name);
            }
        }
        
        public bool Invoke(Jeu jeu, Joueur player)
        {
            WriteMenu(jeu, player);
            
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (Index < Options.Count - 1)
                    {
                        ++Index;
                        WriteMenu(jeu, player);
                    }
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (Index > 0)
                    {
                        --Index;
                        WriteMenu(jeu, player);
                    }
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    Options[Index].Function.Invoke();
                    if (Options[Index].Function.Method.Name == "ExitMenu") return false;
                    Index = 0;
                    break;
                }
            }
            while (key.Key != ConsoleKey.X);

            Console.ReadKey();
            return true;
        }
    }
}