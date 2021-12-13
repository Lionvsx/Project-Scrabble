using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class MainMenu : Menu
    {

        private Jeu _jeu;
        private int turn = 0;
        private int playerTurn = 0;

        public MainMenu()
        {
            this.Options = new List<Option>()
            {
                new Option("Start Game", StartGame),
                new Option("Exit", () => Environment.Exit(0))
            };
            this.Description = "Menu Principal";
        }

        private void StartGame()
        {
            Console.Clear();
            var nbPlayers = int.Parse(Functions.Promp("Entrez un nombre de joueurs : \n"));
            var playerList = new List<Joueur>();
            for (int i = 0; i < nbPlayers; i++)
            {

                var player = new Joueur(Functions.Promp($"Entrez le nom du joueur {i + 1} : \n"));
                player.Menu = new PlayerMenu(player, new List<Option>()
                {
                    new Option("Placer un mot", PlaceWord),
                    new Option("Passer son tour", SkipTurn),
                    new Option("Retour au menu principal", ExitMenu),
                    new Option("Quitter le jeu", () => Environment.Exit(0))
                });
                playerList.Add(player);
            }

            _jeu = new Jeu(playerList);
            NextTurn();

            Invoke();
        }

        private void NextTurn()
        {
            while (playerTurn < _jeu.Players.Count)
            {
                var player = _jeu.Players[playerTurn];
                var exit = player.Menu.Invoke();
                playerTurn++;
                if (exit == false) return;
            }

            playerTurn = 0;
            turn++;
        }

        public void SaveGame()
        {
            
        }

        public void LoadGame()
        {
            
        }
        
        public void PlaceWord()
        {
            
        }

        public void SkipTurn()
        {
            
        }
    }
}