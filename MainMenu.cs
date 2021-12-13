using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class MainMenu : Menu
    {

        private Jeu _jeu;

        public MainMenu()
        {
            this.Options = new List<Option>()
            {
                new Option("Start Game", () => StartGame()),
                new Option("Exit", () => Environment.Exit(0))
            };
            this.Description = "Menu Principal";
        }

        public void StartGame()
        {
            Console.Clear();
            Console.WriteLine("Works");
        }

        public void SaveGame()
        {
            
        }

        public void LoadGame()
        {
            
        }

        public void CreatePlayerMenu(Joueur player)
        {
            
        }

    }
}