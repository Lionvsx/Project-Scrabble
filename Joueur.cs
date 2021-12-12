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

        public Joueur(string name)
        {
            this.words = new List<string>();
            this.score = 0;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
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

        public void Remove_Main_Courante(Jeton monjeton)
        {
            this.mainCourante.Remove(monjeton);
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }

        public List<string> Words
        {
            get => words;
            set => words = value;
        }

        public List<Jeton> MainCourante
        {
            get => mainCourante;
            set => mainCourante = value;
        }
    }
}