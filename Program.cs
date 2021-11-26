using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleHashingApp
{


    class Program
    {
        static void Main(string[] args)
        {

            String Version = "Version Alpha 0.02";


            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|   Welcome to ConsoleHashingApp                 |");
            Console.WriteLine("|   Made by Lunar HUE                            |");
            Console.WriteLine("|   LunarHUE.com                                 |");
            Console.WriteLine("|                                                |");
            Console.WriteLine($"|   {Version}                           |");
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("Enter command. Use help for a list of commands");

            Console.WriteLine();

            String Command = Console.ReadLine().ToLower();

            while (!Command.ToLower().Equals("quit"))
            {
                switch (Command)
                {
                    case string s when s.ToLower().StartsWith("help"):
                        Console.WriteLine("");
                        Console.WriteLine("Command     | Syntax                 | Usage");
                        Console.WriteLine("---------------------------------------------------------------------------------------");
                        Console.WriteLine("Hash        | Hash (plaintext)       | Plaintext into a Hash using SHA256");
                        Console.WriteLine("Quit        | Quit                   | Quit the program");
                        Console.WriteLine("Hashfile    | Hashfile (Directory)   | Files into a Hash using SHA256");
                        Console.WriteLine("OutputFile  | OutputFile (Directory) | Output the commands and outputs of this instance");
                        Console.WriteLine();
                        Console.WriteLine("Commands are NOT case sensitive");
                        Console.WriteLine();
                        break;

                    case string s when s.ToLower().StartsWith("hash "):
                        string output = Hash(s.Substring("hash ".Length));
                        
                        Console.WriteLine($"Hast: {output}");
                        Console.WriteLine();

                        break;

                    case string s when s.ToLower().StartsWith("hashfile "):
                        Console.WriteLine($"Command (Hashfile) not yet implimented");
                        Console.WriteLine("Please wait for an updated version");
                        Console.WriteLine();

                        break;


                    default:
                        Console.WriteLine("Invalid command. Use help for list of commands");
                        Console.WriteLine();
                        break;
                }
                Command = Console.ReadLine();
            }

            string ConsoleLine = String.Empty;


        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string Hash(string input)
        {
            using (SHA256 mySHA256 = SHA256.Create())
            {
                byte[] myArray = Encoding.ASCII.GetBytes(input);
                byte[] hashValue = mySHA256.ComputeHash(myArray);
                return ByteArrayToString(hashValue);
            }
        }
    }
}
