using System.Collections.Generic;

namespace TD_Scrabble
{
    public class Dictionnaire
    {
        private string _lang;
        private int _wordLength;
        private List<string> _words;

        public Dictionnaire(string lang, int wordLength, List<string> words)
        {
            this._lang = lang;
            this._wordLength = wordLength;
            this._words = words;
        }

        public override string ToString()
        {
            return $"Langue: {this._lang}\nLongueur mots: {this._wordLength}\nNombre mots: {this._words.Count}";
        }
        
        
    }
}