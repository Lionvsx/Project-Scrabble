using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class Jeu
    {
        private Case[,] _board;
        private List<Joueur> _players;
        private SacJetons _bag;
        private List<Dictionnaire> _dictionnaries;


        public Jeu(int nbPlayers)
        {
            _players = new List<Joueur>();
            _bag = new SacJetons();
            _dictionnaries = new List<Dictionnaire>();

            var standardCase = new Case(1, 1);
            var doubleLetterCase = new Case(1, 2);
            var tripleLetterCase = new Case(1, 3);
            var doubleWordCase = new Case(2, 1);
            var tripleWordCase = new Case(3, 1);

            _board = new Case[15, 15];

            var boardScoreLines = Functions.ReadFile("../../../BoardScore.txt");

            var indexLine = 0;
            var indexCol = 0;
            foreach (var line in boardScoreLines)
            {
                var args = line.Split(';');
                foreach (var arg in args)
                {
                    _board[indexLine, indexCol] = arg switch
                    {
                        "3*" => tripleWordCase,
                        "2*" => doubleWordCase,
                        "3" => tripleLetterCase,
                        "2" => doubleLetterCase,
                        _ => standardCase
                    };
                    ++indexCol;
                }

                indexCol = 0;
                ++indexLine;
            }
            var dictionnaryLines = Functions.ReadFile("../../../Francais.txt");
            var wordsToAdd = new List<string>();
            foreach (var line in dictionnaryLines)
            {
                if (int.TryParse(line, out int value))
                {
                    Console.WriteLine(wordsToAdd.Count);
                    if (wordsToAdd.Count != 0) _dictionnaries.Add(new Dictionnaire("fr", value, wordsToAdd));
                    wordsToAdd.Clear();
                }
                else
                {
                    wordsToAdd.AddRange(line?.Split(' ')!);
                }
            }


            if (nbPlayers is <= 0 or > 4) throw new ArgumentOutOfRangeException(nameof(nbPlayers));
            for (int i = 0; i < nbPlayers; i++)
            {
                _players.Add(new Joueur($"Player {i+1}"));
            }
        }
        
        public Jeu(int nbPlayers, int nbBot)
        {
            
        }

        public Jeu(string boardSavePath, string playersSavePath)
        {
            
        }

        public void DisplayBoard()
        {
            for (int line = 0; line < _board.GetLength(0); line++)
            {
                for (int col = 0; col < _board.GetLength(1); col++)
                {
                    //Console.SetCursorPosition(line, col);
                    var letterScoreMultiplier = _board[line, col].LetterScoreMultiplier;
                    var wordScoreMultiplier = _board[line, col].WordScoreMultiplier;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = wordScoreMultiplier == 3 ? ConsoleColor.Red :
                        wordScoreMultiplier == 2 ? ConsoleColor.Magenta :
                        letterScoreMultiplier == 3 ? ConsoleColor.DarkCyan :
                        letterScoreMultiplier == 2 ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write(_board[line, col].Letter);
                    Console.Write(" ");
                    
                }
                Console.WriteLine();
            }
        }

        public Case[,] Board
        {
            get => _board;
        }

        public List<Joueur> Players
        {
            get => _players;
            set => _players = value;
        }

        public SacJetons Bag
        {
            get => _bag;
            set => _bag = value;
        }

        public List<Dictionnaire> Dictionnaries
        {
            get => _dictionnaries;
            set => _dictionnaries = value;
        }
    }
}