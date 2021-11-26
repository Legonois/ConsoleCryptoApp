using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleHashingApp
{


    class Program
    {
        static void Main(string[] args)
        {
            string ConsoleLine = String.Empty;

            using (SHA256 mySHA256 = SHA256.Create())
            {
                while (!string.IsNullOrWhiteSpace(ConsoleLine = Console.ReadLine()))
                {
                    Console.WriteLine();
                    Console.WriteLine($"Orig: {ConsoleLine}");

                    byte[] myArray = Encoding.ASCII.GetBytes(ConsoleLine);

                    byte[] hashValue = mySHA256.ComputeHash(myArray);

                    Console.WriteLine($"Hash: {ByteArrayToString(hashValue)}");
                    Console.WriteLine();
                };
            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }
    }
}
