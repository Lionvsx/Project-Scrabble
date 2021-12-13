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
            Jeu.LoadSave("../../../Test.txt");
            Jeu.DisplayBoard();
            
            Console.WriteLine(Jeu.TestPosition(2, 0, "serre", "Player 1", 'd'));
        }
    }
}