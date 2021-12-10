namespace TD_Scrabble
{
    public class Case
    {
        private int wordScoreMultiplier;
        private int letterScoreMultiplier;
        private char letter;

        public Case(int wordScoreMultiplier, int letterScoreMultiplier, char letter)
        {
            this.wordScoreMultiplier = wordScoreMultiplier;
            this.letterScoreMultiplier = letterScoreMultiplier;
            this.letter = letter;
        }

        public Case(int wordScoreMultiplier, int letterScoreMultiplier)
        {
            this.wordScoreMultiplier = wordScoreMultiplier;
            this.letterScoreMultiplier = letterScoreMultiplier;
        }

        public int WordScoreMultiplier => wordScoreMultiplier;

        public int LetterScoreMultiplier => letterScoreMultiplier;

        public char Letter => letter;
    }
}