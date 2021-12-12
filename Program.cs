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
            Jeu.LoadSave("../../../InstancePlateau.txt");
            Jeu.DisplayBoard();
        }
    }
}