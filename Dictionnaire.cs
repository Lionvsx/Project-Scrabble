using System;
using System.Collections.Generic;
using System.Linq;

namespace TD_Scrabble
{
    public class Dictionnaire
    {
        private string _lang;
        private int _wordLength;
        private List<string> _words;
        /// <summary>
        /// Constructeur de la classe Dictionnaire pour les variables d'instances suivantes
        /// </summary>
        /// <param name="lang"> string représentant la langue des mots </param>
        /// <param name="wordLength"> entier représentant la longueur des mots </param>
        /// <param name="words"> Liste représentant un dictionnaire pour une longueur de mot </param>

        public Dictionnaire(string lang, int wordLength, List<string> words)
        {
            this._lang = lang;
            this._wordLength = wordLength;
            this._words = words;
        }

        public Dictionnaire(Dictionnaire dico)
        {
            this._lang = dico._lang;
            this._words = dico._words;
            this._wordLength = dico._wordLength;
        }
        /// <summary>
        /// methode qui décrit le dictionnaire : nombre de mots, longueur des mots, la langue
        /// </summary>
        /// <returns> Une chaine de caractère décrivant le dictionnaire </returns>

        public override string ToString()
        {
            return $"Langue: {this._lang}\nLongueur mots: {this._wordLength}\nNombre mots: {this._words.Count}";
        }

        public bool Includes(string word)
        {
            return _words.Contains(word.ToUpper());
        }

        public string Lang => _lang;

        public int WordLength => _wordLength;

        public List<string> Words => _words;
    }
}