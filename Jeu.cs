using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

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
            

            InitBoard();
            LoadDictionaries();
            
            


            if (nbPlayers is <= 0 or > 4) throw new ArgumentOutOfRangeException(nameof(nbPlayers));
            for (int i = 0; i < nbPlayers; i++)
            {
                _players.Add(new Joueur($"Player {i+1}"));
            }
        }
        
        public Jeu(IEnumerable<Joueur> playerList)
        {
            _players = new List<Joueur>(playerList);
            _bag = new SacJetons();
            

            InitBoard();
            LoadDictionaries();
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

        public void LoadDictionaries()
        {
            _dictionnaries = new List<Dictionnaire>();
            
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
                    if (arg != "_")
                    {
                        var jeton = _bag.RemoveJeton(char.Parse(arg));
                        _board[indexLine, indexCol].Jeton = jeton;
                    }
                    else
                    {
                        _board[indexLine, indexCol].Letter = ' ';
                    }
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
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = wordScoreMultiplier == 3 ? ConsoleColor.DarkRed :
                        wordScoreMultiplier == 2 ? ConsoleColor.Red :
                        letterScoreMultiplier == 3 ? ConsoleColor.DarkCyan :
                        letterScoreMultiplier == 2 ? ConsoleColor.Cyan : ConsoleColor.Gray;
                    Console.Write(_board[line, col].Letter);
                    Console.Write(" ");
                    
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGray;
        }

        public bool TestPosition(int x, int y, string word, Joueur player, char direction)
        {
            if (direction is not ('d' or 'r')) throw new ArgumentOutOfRangeException(nameof(direction));
            //Check if position is withing board
            if (x >= _board.GetLength(1) || x < 0 || y < 0 || y >= _board.GetLength(0)) return false;
            

            if (!CheckWordValid(word)) return false;
            
            //Check if player has required Jetons 
            var requiredJetons = GetNeededJeton(x, y, word, direction);
            foreach (var character in requiredJetons)
            {
                var handJeton = player.MainCourante.Find(jeton => jeton.Id == char.ToUpper(character));
                if (handJeton == null) return false;
            }
            
            if (direction == 'r')
            {
                if (!CheckHorizontalWord(x, y, word, player)) return false;
            }
            else
            {
                if (!CheckVerticalWord(x, y, word, player)) return false;
            }
            player.Add_Mot(word, direction, x, y);
            return true;

        }

        private List<char> GetNeededJeton(int x, int y, string word, char direction)
        {
            var requiredJetons = new List<char>();
            foreach (var character in word)
            {
                if (_board[y, x].Letter == ' ') requiredJetons.Add(character);
                if (direction == 'd') ++y;
                else ++x;
            }

            return requiredJetons;
        }

        private bool CheckHorizontalWord(int x, int y, string word, Joueur player)
        {
            int wordIndex = 0;
            for (int index = x; index < x + word.Length; index++)
            {
                char localLetter = char.ToUpper(word[wordIndex]);
                ++wordIndex;
                Case localCase = _board[y, index];
                Case proximityCaseUp = y > 0 ? _board[y - 1, index] : null;
                Case proximityCaseDown = y < 14 ? _board[y + 1, index] : null;

                if (proximityCaseUp != null && proximityCaseDown != null && localCase.Letter == ' ' && (proximityCaseDown.Letter != ' ' || proximityCaseUp.Letter != ' '))
                {
                    var playerWord = player.InitWord('d');
                    var newWord = DiscoverVerticalWord(index, y, localLetter, playerWord);
                    if (!CheckWordValid(newWord)) return false;
                    playerWord.Status = "valid";
                }

                if (localCase.Letter != ' ' && localCase.Letter != localLetter) return false;
            }

            return true;
        }
        
        private bool CheckVerticalWord(int x, int y, string word, Joueur player)
        {
            int wordIndex = 0;
            for (int index = y; index < y + word.Length; index++)
            {
                char localLetter = char.ToUpper(word[wordIndex]);
                ++wordIndex;
                Case localCase = _board[index, x];
                Case proximityCaseLeft = x > 0 ? _board[index, x - 1] : null;
                Case proximityCaseRight = x < 14 ? _board[index, x + 1] : null;

                if (proximityCaseRight != null && proximityCaseLeft != null && localCase.Letter == ' ' && (proximityCaseLeft.Letter != ' ' || proximityCaseRight.Letter != ' '))
                {
                    var playerWord = player.InitWord('r');
                    var newWord = DiscoverHorizontalWord(x, index, localLetter, playerWord);
                    if (!CheckWordValid(newWord)) return false;
                    playerWord.Status = "valid";
                }
                if (localCase.Letter != ' ' && localCase.Letter != localLetter) return false;
            }

            return true;
        }

        public string DiscoverVerticalWord(int x, int y, char placeholderLetter, PlayerWord playerWord)
        {
            var index = y;
            var discovery = new List<char>();
            _board[y, x].Letter = placeholderLetter;
            while (index > 0 && _board[index-1, x].Letter != ' ')
            {
                index -= 1;
            }

            playerWord.StartingLine = index;
            playerWord.StartingColumn = x;
            

            while (_board[index, x].Letter != ' ')
            {
                discovery.Add(_board[index, x].Letter);
                index += 1;
            }
            _board[y, x].Letter = ' ';
            var discoveryWord = new string(discovery.ToArray());
            playerWord.Word = discoveryWord;
            
            return discoveryWord;
        }
        
        private string DiscoverHorizontalWord(int x, int y, char placeholderLetter, PlayerWord playerWord)
        {
            var index = x;
            var discovery = new List<char>();
            _board[y, x].Letter = placeholderLetter;
            while (index > 0 && _board[y, index-1].Letter != ' ')
            {
                index -= 1;
            }
            
            playerWord.StartingLine = y;
            playerWord.StartingColumn = index;
            

            while (_board[y, index].Letter != ' ')
            {
                discovery.Add(_board[y, index].Letter);
                index += 1;
            }
            _board[y, x].Letter = ' ';
            
            var discoveryWord = new string(discovery.ToArray());
            playerWord.Word = discoveryWord;
            
            return discoveryWord;
        }

        public bool CheckWordValid(string word)
        {
            if (word.Length > 15) return false;
            var dico = _dictionnaries.Find(dictionary => dictionary.WordLength == word.Length);
            
            //Check if dictionary has the word
            return dico == null || dico.Includes(word);
        }

        public bool PlaceWord(int x, int y, string word, string playerName, char direction)
        {
            var player = Players.Find(player => player.Name == playerName);
            if (player == null) throw new ArgumentException("playerName is invalid");
            if (!TestPosition(x, y, word, player, direction)) return false;

            var wordsToPlace = player.Words.FindAll(playerWord => playerWord.Status == "valid");
            foreach (var playerWord in wordsToPlace)
            {
                switch (playerWord.Direction)
                {
                    case 'd':
                        for (int index = playerWord.StartingLine;
                            index < playerWord.StartingLine + playerWord.Word.Length;
                            index++)
                        {
                            var character = playerWord.Word[index - playerWord.StartingLine];
                            _board[index, playerWord.StartingColumn].Jeton ??= player.Remove_Main_Courante(character);

                            playerWord.Score += _board[index, playerWord.StartingColumn].LetterScoreMultiplier *
                                                _board[index, playerWord.StartingColumn].Jeton.ScoreValue;
                            playerWord.WordScoreMultiplier *= _board[index, playerWord.StartingColumn]
                                .WordScoreMultiplier;
                        }
                        break;
                    case 'r':
                        for (int index = playerWord.StartingColumn;
                            index < playerWord.StartingColumn + playerWord.Word.Length;
                            index++)
                        {
                            var character = playerWord.Word[index - playerWord.StartingColumn];
                            _board[playerWord.StartingLine, index].Jeton ??= player.Remove_Main_Courante(character);

                            playerWord.Score += _board[playerWord.StartingLine, index].LetterScoreMultiplier *
                                                _board[playerWord.StartingLine, index].Jeton.ScoreValue;
                            playerWord.WordScoreMultiplier *= _board[playerWord.StartingLine, index]
                                .WordScoreMultiplier;
                        }
                        break;
                }

                playerWord.Status = "placed";
                player.Score += playerWord.Score * playerWord.WordScoreMultiplier;
            }

            return true;
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