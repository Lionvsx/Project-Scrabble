using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");
            var Jeu = new Jeu(4);
            Console.WriteLine(Jeu.Dictionnaries.Count);

            foreach (var dico in Jeu.Dictionnaries)
            {
                Console.WriteLine(dico.ToString());
            }
        }
    }
}