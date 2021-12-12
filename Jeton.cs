namespace TD_Scrabble
{
    /// <summary>
    /// Classe Jeton
    /// </summary>
    public class Jeton
    {
        private char _id;
        private int _scoreValue;
    /// <summary>
    /// Constructeur de la classe jeton pour les variables d'instances suivantes
    /// </summary>
    /// <param name="id"> caractère représentant l'identifiant du jeton </param>
    /// <param name="scoreValue"> entier reprsentant me score du jeton </param>


        public Jeton(char id, int scoreValue)
        {
            _id = char.ToUpper(id);
            this._scoreValue = scoreValue;
        }

        public char Id
        {
            get => _id;
            set => _id = value;
        }

        public int ScoreValue
        {
            get => _scoreValue;
            set => _scoreValue = value;
        }
        /// <summary>
        /// Méthode qui décrit la classe jeton 
        /// </summary>
        /// <returns> une chaine de caractère représentant la description du jeton </returns>

        public override string ToString()
        {
            return "La lettre du jeton est: " + this._id + "\nLe score du jeton est :" + this._scoreValue;
        }
        
    }
}