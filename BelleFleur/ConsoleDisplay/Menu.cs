namespace BelleFleur.ConsoleDisplay;

public class Menu
    {
        private List<Option>? _options;
        private int _index = 0;
        private string? _description;

        public Menu(List<Option>? options, string? description)
        {
            _options = options;
            _description = description;
        }

        public Menu()
        {
            
        }
        

        public bool Invoke()
        {
            Console.Clear();
            WriteMenu();
            
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);

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
                    if (_options[_index].Function.Method.Name == "ExitMenu") return false;
                    _index = 0;
                    break;
                }
            }
            while (key.Key != ConsoleKey.X);

            Console.ReadKey();
            return true;
        }
        public void WriteMenu()
        {
            Console.Clear();
            ConsoleFunctions.ClearConsole();
            Console.WriteLine(_description + "\n");
            
            foreach (var option in _options)
            {
                Console.BackgroundColor = _options.IndexOf(option) == _index ? ConsoleColor.White : ConsoleColor.Black;
                Console.ForegroundColor = _options.IndexOf(option) == _index ? ConsoleColor.Black : ConsoleColor.White;
                Console.Write(_options.IndexOf(option) == _index ? "> " : " ");

                Console.WriteLine(option.Name);
            }
        }
        
        public void ExitMenu()
        {
        }
        
        

        public List<Option>? Options
        {
            get => _options;
            set => _options = value;
        }

        public int Index
        {
            get => _index;
            set => _index = value;
        }

        public string? Description
        {
            get => _description;
            set => _description = value;
        }
        
    }

    public class Option
    {
        public string Name { get; set;}
        public Action Function { get; set; }

        public Option(string name, Action function)
        {
            Name = name;
            Function = function;
        }
    }