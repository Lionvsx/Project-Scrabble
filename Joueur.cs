using System;
using System.Collections.Generic;
using System.Linq;

namespace TD_Scrabble
{
    /// <summary>
    /// Class <c>Joueur</c> models a Scrabble Player
    /// </summary>
    public class Joueur
    {
        private string _name;
        private int _score;
        private List<string> _words;
        private List<Jeton> _mainCourante;

        public Joueur(List<Jeton> mainCourante)
        {
            this._mainCourante = mainCourante;
            this._words = new List<string>();
            this._score = 0;
            this._name = ??
        }

        public void Add_Mot(string mot)
        {
            this._words.Add(mot);
        }

        public override string ToString()
        {
            return $"Nom du joueur : {this._name}\nScore : {this._score}\n\nMots trouvés : \n{String.Join('\n', this._words.ToArray())}";
        }

        public void Add_Score(int val)
        {
            this._score += val;
        }

        public void Add_Main_Courante(Jeton monjeton)
        {
            this._mainCourante.Add(monjeton);
        }

        public void Remove_Main_Courage(Jeton monjeton)
        {
            this._mainCourante.Remove(monjeton);
        }
    }
}