namespace TD_Scrabble
{
    public class Case
    {
        private int _wordScoreMultiplier;
        private int _letterScoreMultiplier;
        private char _tempLetter = ' ';
        private Jeton _jeton;

        public Case(int wordScoreMultiplier, int letterScoreMultiplier, char letter)
        {
            this._wordScoreMultiplier = wordScoreMultiplier;
            this._letterScoreMultiplier = letterScoreMultiplier;
            this._tempLetter = char.ToUpper(letter);
        }
        
        public Case(int wordScoreMultiplier, int letterScoreMultiplier, Jeton jeton)
        {
            this._wordScoreMultiplier = wordScoreMultiplier;
            this._letterScoreMultiplier = letterScoreMultiplier;
            this._jeton = jeton;
        }

        public Case(int wordScoreMultiplier, int letterScoreMultiplier)
        {
            this._wordScoreMultiplier = wordScoreMultiplier;
            this._letterScoreMultiplier = letterScoreMultiplier;
        }

        public int WordScoreMultiplier => _wordScoreMultiplier;

        public int LetterScoreMultiplier => _letterScoreMultiplier;

        public char Letter
        {
            get => _jeton?.Id ?? _tempLetter; // Si le jeton existe return id du jeton sinon return tempLetter
            set => _tempLetter = value;
        }

        public Jeton Jeton
        {
            get => _jeton;
            set => _jeton = value;
        }

        public Case Duplicate()
        {
            return new Case(this._wordScoreMultiplier, this._letterScoreMultiplier, this.Letter);
        }
    }
}