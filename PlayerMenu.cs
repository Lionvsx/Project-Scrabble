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
            Console.Clear();
            jeu.DisplayBoard();
            player.DisplayPlayerInfo();
            Console.WriteLine(Description + "\n");
            
            foreach (var option in Options)
            {
                Console.Write(Options.IndexOf(option) == Index ? "> " : " ");

                Console.WriteLine(option.Name);
            }
        }
    }
}