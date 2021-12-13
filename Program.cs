using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainMenu = new MainMenu();
            mainMenu.Invoke();
            
            
            var jeu = new Jeu(4);
            jeu.LoadSave("../../../Test.txt");
            jeu.DisplayBoard();
            var joueur1 = jeu.Players.Find(player => player.Name == "Player 1");
            
            joueur1.Add_Main_Courante(new Jeton('e', 1));
            joueur1.Add_Main_Courante(new Jeton('r', 1));
            joueur1.Add_Main_Courante(new Jeton('r', 1));
            joueur1.Add_Main_Courante(new Jeton('e', 1));
            
            
            
            
            Console.WriteLine(jeu.PlaceWord(2, 0, "serre", "Player 1", 'd'));
            
            Console.WriteLine(joueur1.ToString());
            jeu.DisplayBoard();
        }
    }
}