namespace TD_Scrabble
{
    public class DicoJeton : Jeton
    {
        private int count;

        public DicoJeton(char id, int scoreValue, int count) : base(id, scoreValue)
        {
            this.count = count;
        }
    }
}