using System;
using System.Runtime.CompilerServices;

namespace TD_Scrabble
{
    public class Plateau
    {
        private char[,] _content;
        
        public override string ToString()
        {
            for (int i = 0; i < _content.GetLength(0); i++)
            {
                for (int j = 0; j < _content.GetLength(1); j++)
                {
                    Console.WriteLine('T');
                }
            }
        }
        
    }
}