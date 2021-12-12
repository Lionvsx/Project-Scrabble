using System;
using System.Collections.Generic;
using System.Linq;

namespace TD_Scrabble
{
    public class Jeu
    {
        private Case[,] _board;
        private List<Joueur> _players;
        private SacJetons _bag;
        private List<Dictionnaire> _dictionnaries;
        /// <summary>
        /// Constructeur de la classe Jeu
        /// </summary>
        /// <param name="nbPlayers"> Entier représentant le nombre de joueurs</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>


        public Jeu(int nbPlayers)
        {
            _players = new List<Joueur>();
            _bag = new SacJetons();
            _dictionnaries = new List<Dictionnaire>();

            InitBoard();

            
            var dictionaryLines = Functions.ReadFile("../../../Francais.txt").Reverse();
            var wordsToAdd = new List<string>();
            foreach (var line in dictionaryLines)
            {
                if (int.TryParse(line, out int value))
                {
                    _dictionnaries.Add(new Dictionnaire("fr", value, wordsToAdd));
                    wordsToAdd = new List<string>();
                }
                else
                {
                    wordsToAdd.AddRange(line.Split(' '));
                }
            }

            _dictionnaries.Reverse();


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

        public void InitBoard()
        {
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
                        "3*" => tripleWordCase.Duplicate(),
                        "2*" => doubleWordCase.Duplicate(),
                        "3" => tripleLetterCase.Duplicate(),
                        "2" => doubleLetterCase.Duplicate(),
                        _ => standardCase.Duplicate()
                    };
                    ++indexCol;
                }

                indexCol = 0;
                ++indexLine;
            }
        }

        public void LoadSave(string path)
        {
            var boardLines = Functions.ReadFile(path);
            var indexLine = 0;
            var indexCol = 0;
            foreach (var line in boardLines)
            {
                var args = line.Split(';');
                foreach (var arg in args)
                {
                    _board[indexLine, indexCol].Letter = arg == "_" ? ' ' : char.Parse(arg);; 
                    ++indexCol;
                }

                indexCol = 0;
                ++indexLine;
            }
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
                    //Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = wordScoreMultiplier == 3 ? ConsoleColor.DarkRed :
                        wordScoreMultiplier == 2 ? ConsoleColor.Red :
                        letterScoreMultiplier == 3 ? ConsoleColor.DarkCyan :
                        letterScoreMultiplier == 2 ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write(_board[line, col].Letter);
                    Console.Write(" ");
                    
                }
                Console.WriteLine();
            }
        }

        public bool TestPosition(int x, int y, string word, Joueur player, char direction)
        {
            if (direction is not ('d' or 'r')) throw new ArgumentOutOfRangeException(nameof(direction));
            //Check if position is withing board
            if (x >= _board.GetLength(1) || x < 0 || y < 0 || y > _board.GetLength(0)) return false;
            

            if (!CheckWordValid(word)) return false;

            //Check if player has required Jetons 
            foreach (var character in word)
            {
                if (player.MainCourante.Find(jeton => jeton.Id == char.ToUpper(character)) == null) return false;
            }


            return false;

        }

        private bool CheckHorizontalWord(int x, int y, string word)
        {
            int wordIndex = 0;
            for (int index = x; index < x + word.Length; index++)
            {
                char localLetter = word[wordIndex];
                ++wordIndex;
                Case localCase = _board[y, index];
                Case proximityCaseUp = y > 0 ? _board[y - 1, index] : null;
                Case proximityCaseDown = y < 14 ? _board[y + 1, index] : null;

                if (localCase.Letter == ' ' && (proximityCaseDown?.Letter != ' ' || proximityCaseUp?.Letter != ' '))
                {
                    var newWord = DiscoverVerticalWord(index, y, localLetter);
                    if (!CheckWordValid(newWord)) return false;
                }
            }

            return true;
        }
        
        private bool CheckVerticalWord(int x, int y, string word)
        {
            return true;
        }

        private string DiscoverVerticalWord(int x, int y, char placeholderLetter)
        {
            var startingPosition = y;
            var discovery = new List<char>();
            while (_board[y-1, x].Letter != ' ' && y > 0)
            {
                y -= 1;
            }

            while (_board[y, x].Letter != ' ')
            {
                y += 1;
                if (y == startingPosition)
                {
                    discovery.Add(placeholderLetter);
                    continue;
                }
                discovery.Add(_board[y, x].Letter);
            }

            return new string(discovery.ToArray());
        }
        
        private string DiscoverHorizontalWord(int x, int y, char placeholderLetter)
        {
            var startingPosition = x;
            var discovery = new List<char>();
            while (_board[y, x-1].Letter != ' ' && x > 0)
            {
                x -= 1;
            }

            while (_board[y, x].Letter != ' ')
            {
                x += 1;
                if (x == startingPosition)
                {
                    discovery.Add(placeholderLetter);
                    continue;
                }
                discovery.Add(_board[y, x].Letter);
            }
            return new string(discovery.ToArray());
        }

        private bool CheckWordValid(string word)
        {
            if (word.Length > 15) return false;
            var dico = _dictionnaries.Find(dictionary => dictionary.WordLength == word.Length);
            
            //Check if dictionary has the word
            return dico == null || dico.Includes(word);
        }
        
        

        public Case[,] Board
        {
            get => _board;
        }

        public List<Joueur> Players
        {
            get => _players;
        }

        public SacJetons Bag
        {
            get => _bag;
        }

        public List<Dictionnaire> Dictionnaries
        {
            get => _dictionnaries;
        }
    }
}