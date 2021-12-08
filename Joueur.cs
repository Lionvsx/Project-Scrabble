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
        private string name;
        private int score;
        private List<string> words;
        private List<Jeton> mainCourante;

        public Joueur()
        {
            this.words = new List<string>();
            this.score = 0;
            this.name = ??
        }

        public void Add_Mot(string mot)
        {
            this.words.Add(mot);
        }

        public override string ToString()
        {
            return $"Nom du joueur : {this.name}\nScore : {this.score}\n\nMots trouvés : \n{String.Join('\n', this.words.ToArray())}";
        }

        public void Add_Score(int val)
        {
            this.score += val;
        }

        public void Add_Main_Courante(Jeton monjeton)
        {
            this.mainCourante.Add(monjeton);
        }

        public void Remove_Main_Courage(Jeton monjeton)
        {
            this.mainCourante.Remove(monjeton);
        }
    }
}