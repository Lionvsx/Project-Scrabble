namespace TD_Scrabble
{
    public class Case
    {
        private int wordScoreMultiplier;
        private int letterScoreMultiplier;
        private char tempLetter;
        private Jeton jeton;

        public Case(int wordScoreMultiplier, int letterScoreMultiplier, char letter)
        {
            this.wordScoreMultiplier = wordScoreMultiplier;
            this.letterScoreMultiplier = letterScoreMultiplier;
            this.tempLetter = char.ToUpper(letter);
        }
        
        public Case(int wordScoreMultiplier, int letterScoreMultiplier, Jeton jeton)
        {
            this.wordScoreMultiplier = wordScoreMultiplier;
            this.letterScoreMultiplier = letterScoreMultiplier;
            this.jeton = jeton;
        }

        public Case(int wordScoreMultiplier, int letterScoreMultiplier)
        {
            this.wordScoreMultiplier = wordScoreMultiplier;
            this.letterScoreMultiplier = letterScoreMultiplier;
        }

        public int WordScoreMultiplier => wordScoreMultiplier;

        public int LetterScoreMultiplier => letterScoreMultiplier;

        public char Letter
        {
            get => jeton?.Id ?? tempLetter;
            set => tempLetter = value;
        }

        public Jeton Jeton
        {
            get => jeton;
            set => jeton = value;
        }

        public Case Duplicate()
        {
            return new Case(this.wordScoreMultiplier, this.letterScoreMultiplier, this.Letter);
        }
    }
}