using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class Jeu
    {
        private char[,] _board;
        private List<Joueur> _players;
        private SacJetons _bag;
        private List<Dictionnaire> _dictionnaries;


        public Jeu(int nbPlayers)
        {
            _players = new List<Joueur>();
            _bag = new SacJetons();
            _dictionnaries = new List<Dictionnaire>();

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
        
        public Jeu(int nbPlayers, int nbIA)
        {
            
        }

        public Jeu(string boardSavePath, string playersSavePath)
        {
        }

        public char[,] Board
        {
            get => _board;
            set => _board = value;
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