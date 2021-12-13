namespace TD_Scrabble
{
    public class PlayerWord
    {
        private int _startingColumn;
        private int _startingLine;
        private string _word;
        private string _status;
        private char _direction;
        private int score = 0;
        private int wordScoreMultiplier = 1;
        
        public PlayerWord(char direction)
        {
            this._status = "pending";
            this._direction = direction;
        }

        public PlayerWord(int startingColumn, int startingLine, string word, string status, char direction)
        {
            _startingColumn = startingColumn;
            _startingLine = startingLine;
            _word = word.ToUpper();
            _status = status;
            _direction = direction;
        }

        public int StartingColumn
        {
            get => _startingColumn;
            set => _startingColumn = value;
        }

        public int StartingLine
        {
            get => _startingLine;
            set => _startingLine = value;
        }

        public string Word
        {
            get => _word;
            set => _word = value.ToUpper();
        }

        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public char Direction
        {
            get => _direction;
            set => _direction = value;
        }

        public string ToStringDebug()
        {
            return $"X : {_startingColumn} Y : {_startingLine}  Mot : {_word}  Statut : {_status}  Score : {score}  Mot compte : x{wordScoreMultiplier}";
        }
        public override string ToString()
        {
            return $"Mot : {_word}  Score : {score*wordScoreMultiplier}";
        }

        public int Score
        {
            get => score;
            set => score = value;
        }

        public int WordScoreMultiplier
        {
            get => wordScoreMultiplier;
            set => wordScoreMultiplier = value;
        }
    }
}