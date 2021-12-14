using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class MainMenu : Menu
    {

        private Jeu _jeu;
        private int turn = 0;
        private int playerTurn = 0;
        private List<Joueur> _playerList;

        public MainMenu()
        {
            this.Options = new List<Option>()
            {
                new Option("Commencer une nouvelle partie", StartGame),
                new Option("Reprendre la partie", ResumeGame),
                new Option("Charger une sauvegarde", LoadGame),
                new Option("Sauvegarder la partie", SaveGame),
                new Option("Exit", () => Environment.Exit(0))
            };
            this.Description = "Menu Principal";
        }

        private void StartGame()
        {
            Console.Clear();
            var nbPlayers = int.Parse(Functions.Prompt("Entrez un nombre de joueurs : \n"));
            var playerList = new List<Joueur>();
            for (int i = 0; i < nbPlayers; i++)
            {

                var player = new Joueur(Functions.Prompt($"Entrez le nom du joueur {i + 1} : \n"));
                player.Menu = new PlayerMenu(player, new List<Option>()
                {
                    new Option("Placer un mot", PlaceWord),
                    new Option("Passer son tour", SkipTurn),
                    new Option("Changer sa main", ResetHand),
                    new Option("Retour au menu principal", ExitMenu),
                    new Option("Quitter le jeu", () => Environment.Exit(0))
                });
                playerList.Add(player);
            }

            _jeu = new Jeu(playerList);
            _jeu.InitPlayerHands();
            _playerList = playerList;
            NextTurn();

            Invoke();
        }

        private void ResumeGame()
        {
            if (_jeu == null)
            {
                Functions.ClearConsole();
                Console.WriteLine("Aucune partie n'est en cours !");
                Console.ReadKey();
                Invoke();
            }
            else
            {
                NextTurn();

                Invoke();
            }
        }

        private void NextTurn()
        {
            while (playerTurn < _jeu.Players.Count)
            {
                var player = _jeu.Players[playerTurn];
                var exit = player.Menu.Invoke(_jeu, player);
                if (exit == false) return;
                playerTurn++;
            }

            playerTurn = 0;
            turn++;
            NextTurn();
        }

        public void SaveGame()
        {
            _jeu.SaveGame();
            Console.Clear();
            Console.WriteLine("Partie sauvegardée avec succes..");
        }

        public void LoadGame()
        {
            var players = new List<Joueur>();
            var playerLines = Functions.ReadFile("../../../PlayerSave.txt");
            foreach (var line in playerLines)
            {
                var blocs = line.Split(';');
                var playerName = blocs[0].Split('|')[0];
                var playerScore = blocs[0].Split('|')[1];
                var player = new Joueur(playerName);
                player.Score = int.Parse(playerScore);
                player.Menu = new PlayerMenu(player, new List<Option>()
                {
                    new Option("Placer un mot", PlaceWord),
                    new Option("Passer son tour", SkipTurn),
                    new Option("Changer sa main", ResetHand),
                    new Option("Retour au menu principal", ExitMenu),
                    new Option("Quitter le jeu", () => Environment.Exit(0))
                });
                players.Add(player);
            }

            _jeu = new Jeu(players);
            foreach (var line in playerLines)
            {
                var blocs = line.Split(';');
                var player = _jeu.Players.Find(player => player.Name == blocs[0].Split('|')[0]);
                
                
                foreach (var jetonChar in blocs[1].Split('|'))
                {
                    if (!char.TryParse(jetonChar, out char result)) continue;
                    
                    var jeton = _jeu.Bag.RemoveJeton(char.Parse(jetonChar));
                    player?.Add_Main_Courante(jeton);
                }

                for (int i = 2; i < blocs.Length - 1; i++)
                {
                    var playerWordArgs = blocs[i].Split('|');
                    var playerWord = player.InitWord(char.Parse(playerWordArgs[5]));
                    playerWord.Score = int.Parse(playerWordArgs[1]);
                    playerWord.Word = playerWordArgs[0];
                    playerWord.WordScoreMultiplier = int.Parse(playerWordArgs[2]);
                    playerWord.StartingLine = int.Parse(playerWordArgs[3]);
                    playerWord.StartingColumn = int.Parse(playerWordArgs[4]);
                    playerWord.Status = "placed";
                }
            }
            _jeu.LoadSave();
            Console.Clear();
            Console.WriteLine("Sauvegarde chargée avec succès..");
            Console.ReadKey();
            Invoke();
        }
        
        
        public void PlaceWord()
        {
            if (_jeu.Bag.Content.Count == 0)
            {
                Console.Clear();
                Console.WriteLine($"Partie terminée, plus aucun jeton disponible dans le sac !\n\n====== Leaderboard ======\n{_jeu.GetLeaderboard()}");
                return;
            }
            Console.Clear();
            Console.WriteLine(_jeu.Players[playerTurn].ToString());
            var word = Functions.Prompt("Entrez un mot que vous voulez poser sur le plateau : \n");
            var x = int.Parse(Functions.Prompt("A quelle colonne voulez vous rentrer votre mot? (Le plateau commence à la colonne 1)\n")) - 1;
            var y = int.Parse(Functions.Prompt("A quelle ligne voulez vous rentrer votre mot? (Le plateau commence à la ligne 1)\n")) - 1 ;
            Console.WriteLine("Dans quelle direction voulez vous écrire votre mot? (Utilisez les flèches du clavier)");
            var key = Console.ReadKey();

            char direction = key.Key == ConsoleKey.DownArrow ? 'd' : key.Key == ConsoleKey.RightArrow ? 'r' : ' ';
            if (direction == ' ')
            {
                Console.WriteLine("Direction invalide, veuillez réessayer\nAppuyez sur n'importe quelle touche pour continuer...");
                PlaceWord();
                return;
            }
            var success = _jeu.PlaceWord(x, y, word, _jeu.Players[playerTurn].Name, direction);
            if (success == false)
            {
                Console.WriteLine("Vous ne pouvez pas placer de mot dans cette configuration !\nAppuyez sur n'importe quelle touche pour continuer...");
                Console.ReadKey();
                PlaceWord();
            }
            else
            {
                Console.WriteLine("Mot placé avec succès :\nAppuyez sur n'importe quelle touche pour continuer...");
                _jeu.DisplayBoard();
            }
        }

        public void ResetHand()
        {
            Functions.ClearConsole();
            _jeu.ResetHand(_jeu.Players[playerTurn]);
            Console.WriteLine("Votre main a été remplacée par de nouveaux jetons venant du sac...");
            
        }

        public void SkipTurn()
        {   
            Functions.ClearConsole();
            Console.WriteLine("Appuyez sur n'importe quel touche pour passer son tour...");
        }
    }
}