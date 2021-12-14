using System;
using System.Collections.Generic;

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
        private PlayerMenu menu;
        
        
        /// <summary>
        /// Constructeur de la classe joueur pour les variables d'instances suivantes
        /// </summary>
        /// <param name="name"> string représentant le nom du joueur</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Joueur(string name)
        {
            words = new List<PlayerWord>();
            score = 0;
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            mainCourante = new List<Jeton>();
        }

        public void Add_Mot(string mot, char direction, int x, int y)
        {
            words.Add(new PlayerWord(x, y, mot, "valid", direction));
        }

        public PlayerWord InitWord(char direction)
        {
            var newWord = new PlayerWord(direction);
            words.Add(newWord);
            return newWord;
        }
       /// <summary>
       /// Méthode qui décrit la classe joueur
       /// </summary>
       /// <returns> Une chaine de caractère qui décrit le joueur </returns>
        public override string ToString()
       {
           var jetonString = "";
           foreach (var jeton in mainCourante)
           {
               jetonString += $"{jeton.Id}({jeton.ScoreValue}) ";
           }
           return $"Nom du joueur : {name}\nScore : {score}\n\nMots trouvés : \n{WordsToString()}\nMain : {jetonString}";
        }
       

       public string WordsToString()
       {
           var result = "";
           var placedWords = words.FindAll(word => word.Status == "placed");
           foreach (var playerWord in placedWords)
           {
               result += playerWord + "\n";
           }

           return result;
       }
       
       public string WordsToSaveString()
       {
           var result = "";
           var placedWords = words.FindAll(word => word.Status == "placed");
           foreach (var playerWord in placedWords)
           {
               result += $"{playerWord.Word}|{playerWord.Score}|{playerWord.WordScoreMultiplier}|{playerWord.StartingLine}|{playerWord.StartingColumn}|{playerWord.Direction}" + ";";
           }

           return result;
       }
       /// <summary>
       /// Méthode qui ajoute une valeur au score
       /// </summary>
       /// <param name="val"> entier représentant la valeur ajoutée au score </param>

        public void Add_Score(int val)
        {
            score += val;
        }
       /// <summary>
       /// Méthode qui permet d'ajouter un jeton à la liste de jetons de la partie en cours
       /// </summary>
       /// <param name="monjeton"> paramètre représentant le jeton à ajouter dans liste mainCourante </param>
        public void Add_Main_Courante(Jeton monjeton)
        {
            mainCourante.Add(monjeton);
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

       public void DisplayPlayerInfo()
       {
           Console.WriteLine("===== INFO JOUEUR =====");
           Console.WriteLine(ToString());
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

        public PlayerMenu Menu
        {
            get => menu;
            set => menu = value;
        }
    }
}