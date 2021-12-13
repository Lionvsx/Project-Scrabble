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
        private List<PlayerWord> words;
        private List<Jeton> mainCourante;
        /// <summary>
        /// Constructeur de la classe joueur pour les variables d'instances suivantes
        /// </summary>
        /// <param name="name"> string représentant le nom du joueur</param>
        /// <exception cref="ArgumentNullException"></exception>


        public Joueur(string name)
        {
            this.words = new List<PlayerWord>();
            this.score = 0;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.mainCourante = new List<Jeton>();
        }
        public void Add_Mot(string mot, char direction, int x, int y)
        {
            this.words.Add(new PlayerWord(x, y, mot, "valid", direction));
        }

        public PlayerWord InitWord(char direction)
        {
            var newWord = new PlayerWord(direction);
            this.words.Add(newWord);
            return newWord;
        }
       /// <summary>
       /// Méthode qui décrit la classe joueur
       /// </summary>
       /// <returns> Une chaine de caractère qui décrit le joueur </returns>
        public override string ToString()
        {
            return $"Nom du joueur : {this.name}\nScore : {this.score}\n\nMots trouvés : \n{this.WordsToString()}";
        }

       public string WordsToString()
       {
           var result = "";
           foreach (var playerWord in words)
           {
               result += playerWord.ToString() + "\n";
           }

           return result;
       }
       /// <summary>
       /// Méthode qui ajoute une valeur au score
       /// </summary>
       /// <param name="val"> entier représentant la valeur ajoutée au score </param>

        public void Add_Score(int val)
        {
            this.score += val;
        }
       /// <summary>
       /// Méthode qui permet d'ajouter un jeton à la liste de jetons de la partie en cours
       /// </summary>
       /// <param name="monjeton"> paramètre représentant le jeton à ajouter dans liste mainCourante </param>
        public void Add_Main_Courante(Jeton monjeton)
        {
            this.mainCourante.Add(monjeton);
        }

       /// <summary>
       /// Méthode qui permet d'enlever un jeton à la liste de jetons de la partie en cours
       /// </summary>
       /// <param name="letter"></param>
       public Jeton Remove_Main_Courante(char letter)
        {
            var selectedJeton = mainCourante.Find(jeton => jeton.Id == char.ToUpper(letter));
            mainCourante.Remove(selectedJeton);
            return selectedJeton;
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
        

        public List<Jeton> MainCourante
        {
            get => mainCourante;
            set => mainCourante = value;
        }

        public List<PlayerWord> Words
        {
            get => words;
            set => words = value;
        }
    }
}