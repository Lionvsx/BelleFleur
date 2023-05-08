namespace BelleFleur.ConsoleDisplay;

public static class ConsoleFunctions
    {
        public static void ClearConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }
        

        public static void DisplayIntChain(int[] chain)
        {
            for (var index = 0; index < chain.Length; index++)
            {
                if (index % 8 == 0 && index != 0) Console.Write(" ");
                var bit = chain[index];
                Console.Write(bit);
            }
            Console.WriteLine();
        }

        public static string ReadPassword()
        {
            var password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        password = password[0..^1];
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
        }
    }