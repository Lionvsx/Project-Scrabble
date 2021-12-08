using System.Collections.Generic;

namespace TD_Scrabble
{
    public class Jeu
    {
        private string[,] board;
        private List<Joueur> players;
        private SacJetons bag;
        private SortedList<int,Dictionnaire> dictionnaries;


        public Jeu(int nbPlayers)
        {
        }

        public Jeu(string boardSavePath, string playersSavePath)
        {
        }
    }
}