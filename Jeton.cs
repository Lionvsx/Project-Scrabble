namespace TD_Scrabble
{
    public class Jeton
    {
        private char _id;
        private int _scoreValue;



        public Jeton(char id, int scoreValue)
        {
            _id = id;
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
        

        public override string ToString()
        {
            return "La lettre du jeton est: " + this._id + "\nLe score du jeton est :" + this._scoreValue;
        }
    }
}