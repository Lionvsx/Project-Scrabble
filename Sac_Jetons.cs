using System;
using System.Collections.Generic;
using System.Linq;

namespace TD_Scrabble
{
    public class SacJetons
    {
        private List<Jeton> _content;
        public SacJetons()
        {
            _content = new List<Jeton>();
            
            var lines = Functions.ReadFile("../../../Jetons.txt");
            foreach (var args in lines.Select(line => line.Split(';')))
            {
                for (int count = 0; count < int.Parse(args[2]); count++)
                {
                    _content.Add(new Jeton(char.Parse(args[0]), int.Parse(args[1])));
                }
            }
        }
        
        public SacJetons(string path)
        {
            _content = new List<Jeton>();
            
            var lines = Functions.ReadFile(path);
            foreach (var args in lines.Select(line => line.Split(';')))
            {
                for (int count = 0; count < int.Parse(args[2]); count++)
                {
                    _content.Add(new Jeton(char.Parse(args[0]), int.Parse(args[1])));
                }
            }
        }

        public List<Jeton> Content
        {
            get => _content;
            set => _content = value;
        }
        
        public Jeton RemoveJeton(char letter)
        {
            var selectedJeton = _content.Find(jeton => jeton.Id == char.ToUpper(letter));
            _content.Remove(selectedJeton);
            return selectedJeton;
        }

        public Jeton TakeRandom()
        {
            var randomInt = Functions.GetRandomInt(0, _content.Count);
            var selectedJeton = _content[randomInt];
            _content.RemoveAt(randomInt); 
            return selectedJeton;
        }
         /// <summary>
         /// Méthode qui permet de décrire le contenu du sac jetons
         /// </summary>
         /// <returns> Une chaine de caractères décrivant le contenu du sac de jetons </returns>
        public override string ToString()
        {
            string s = null;
            for (int i = 0; i < _content.Count; i++)
            {
                s += _content[i].ToString() + "\n";
            }

            return s;
        }
    }
    
    
}