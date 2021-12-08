using System.Collections.Generic;

namespace TD_Scrabble
{
    public class Dictionnaire
    {
        private string _lang;
        private int wordLength;
        private List<string> content;

        public Dictionnaire(string lang, int wordLength)
        {
            this._lang = lang;
            this.wordLength = wordLength;
            var lines = Functions.ReadFile("../../Français.txt");
        }

        public override string ToString()
        {
            return "Nombre mots qui ont comme longueur : " + this.wordLength + " " + "Langue: " + this._lang;
        }
        
        
    }
}