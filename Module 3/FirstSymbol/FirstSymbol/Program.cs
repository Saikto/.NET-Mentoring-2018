using System;

namespace FirstSymbol
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                string currentStr = Console.ReadLine();
                try
                {
                    Console.WriteLine($"First symbol of the string is: '{currentStr[0]}'.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Input string was empty, try again.");
                }
            }
        }
    }
}
