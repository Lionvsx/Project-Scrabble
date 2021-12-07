namespace TD_Scrabble
{
    public class Jeton
    {
        private char _id;
        private int scoreValue;

        public Jeton(char id, int scoreValue)
        {
            _id = id;
            this.scoreValue = scoreValue;
        }

        public char Id
        {
            get => _id;
            set => _id = value;
        }

        public int ScoreValue
        {
            get => scoreValue;
            set => scoreValue = value;
        }
    }
}