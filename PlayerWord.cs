namespace TD_Scrabble
{
    public class PlayerWord
    {
        private int _startingColumn;
        private int _startingLine;
        private string _word;
        private string _status;
        private char _direction;

        public PlayerWord(string word, char direction)
        {
            this._word = word;
            this._status = "pending";
            this._direction = direction;
        }
        public PlayerWord(char direction)
        {
            this._status = "pending";
            this._direction = direction;
        }
        public PlayerWord(string word, char direction, string status)
        {
            this._word = word;
            this._status = status;
            this._direction = direction;
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
            set => _word = value;
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

        public override string ToString()
        {
            return $"X : {_startingColumn} Y : {_startingLine}  Mot : {_word}  Statut : {_status}";
        }
    }
}