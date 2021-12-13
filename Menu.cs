using System;
using System.Collections.Generic;

namespace TD_Scrabble
{
    public class Menu
    {
        private List<Option> _options;
        private int _index = 0;
        private string description;

        public Menu(List<Option> options, string description)
        {
            _options = options;
        }

        public Menu()
        {
        }



        public void Invoke()
        {
            WriteMenu();
            
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                
                if (key.Key == ConsoleKey.DownArrow)
                {
                    if (_index < _options.Count - 1)
                    {
                        ++_index;
                        WriteMenu();
                    }
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    if (_index > 0)
                    {
                        --_index;
                        WriteMenu();
                    }
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    _options[_index].Function.Invoke();
                    _index = 0;
                    break;
                }
            }
            while (key.Key != ConsoleKey.X);

            Console.ReadKey();
        }
        public void WriteMenu()
        {
            Console.Clear();
            Console.WriteLine(description + "\n");
            
            foreach (var option in _options)
            {
                if (_options.IndexOf(option) == _index)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
        }

        public List<Option> Options
        {
            get => _options;
            set => _options = value;
        }

        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }
    }

    public class Option
    {
        public string Name { get; }
        public Action Function { get; }

        public Option(string name, Action function)
        {
            Name = name;
            Function = function;
        }
    }
}