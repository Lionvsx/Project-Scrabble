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
        /// <summary>
        /// Constructeur de la classe joueur pour les variables d'instances suivantes
        /// </summary>
        /// <param name="name"> string représentant le nom du joueur</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="score"> entier représentant le score du joueur </param>
        /// <param name="words"> Liste représentant les mots </param>
        /// <param name="mainCourante"> Liste représentant les jetons de la partie en cours</param>
        

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
       /// <summary>
       /// Méthode qui décrit la classe joueur
       /// </summary>
       /// <returns> Une chaine de caractère qui décrit le joueur </returns>
        public override string ToString()
        {
            return $"Nom du joueur : {this.name}\nScore : {this.score}\n\nMots trouvés : \n{String.Join('\n', this.words.ToArray())}";
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="val"></param>

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
        
    }
}