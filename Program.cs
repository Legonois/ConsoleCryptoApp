using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleCryptoApp
{
    

    class Program
    {
        static void Main(string[] args)
        {

            // WriteNLog is a class that contains both writing and logging to the log
            // file, thus the name. WriteNLog.Write does both Console.Writeline(to
            // display on the command prompt) and mainLog += Environment.NewLine(to
            // add to the mainlog string for later output). 

            WriteNLog.Write(DateTime.Now);

            string Version = "Version Alpha 0.04";
            

            WriteNLog.Write("+------------------------------------------------+");
            WriteNLog.Write("|   Welcome to CryptoConsoleApp                  |");
            WriteNLog.Write("|   Made by Lunar HUE                            |");
            WriteNLog.Write("|   LunarHUE.com                                 |");
            WriteNLog.Write("|                                                |");
            WriteNLog.Write($"|   {Version}                           |");
            WriteNLog.Write("+------------------------------------------------+");
            WriteNLog.Write("Enter command. Use help for a list of commands");
            WriteNLog.Write();

            string command = Console.ReadLine();
            WriteNLog.mainLog += command + Environment.NewLine;

            while (!command.ToLower().Equals("quit"))
            {
                switch (command)
                {
                    case string s when s.ToLower().StartsWith("help"):

                        Console.WriteLine("asidjaskdhoiashdioash");

                        WriteNLog.Write("       Command                          | Description");
                        WriteNLog.Write("       ---------------------------------+-----------------------------------------------------------");
                        WriteNLog.Write("Hashing                                 |");
                        WriteNLog.Write("       Hash (plaintext)                 | Generates a hash from plaintext using SHA256");
                        WriteNLog.Write("       Hashfile (Directory)             | Files into a Hash using SHA256");
                        WriteNLog.Write("       HashPassword (Password)          | Generates a hash from plaintext with user defined salt");
                        WriteNLog.Write("                                        |");
                        WriteNLog.Write("Encryption                              |");
                        WriteNLog.Write("       GenerateKey (Private Key)        | Generates a public key from a private key");
                        WriteNLog.Write("                                        |");
                        WriteNLog.Write("Options                                 |");
                        WriteNLog.Write("       OutputFile (Directory)           | Output the commands and outputs of this instance");
                        WriteNLog.Write("       Quit                             | Exits the program");
                        WriteNLog.Write();
                        WriteNLog.Write("Proof of concept: Mine (Plaintext)      |");
                        WriteNLog.Write();
                        WriteNLog.Write("Commands are NOT case sensitive");
                        WriteNLog.Write();
                        break;

                    case string s when s.ToLower().StartsWith("hash "):
                        string output = Cryptography.Hash(command.Substring("hash ".Length));

                        WriteNLog.Write($"Hash: {output}");
                        WriteNLog.Write();

                        break;

                    case string s when s.ToLower().StartsWith("hashfile "):
                        string hashFilePath = s.Substring("hashfile ".Length);

                        string hashedFile = Cryptography.Hashfile(hashFilePath);
                        if (hashedFile != null)
                        {
                            WriteNLog.Write($"Hashed File: {hashedFile}");
                            WriteNLog.Write();
                        }
                        else
                        {
                            WriteNLog.Write("Check Documentation");
                            WriteNLog.Write();
                        }

                        break;


                    case string s when s.ToLower().StartsWith("outputfile "):
                        string logFilePath = s.Substring("outputfile ".Length);
                        CreateLog(logFilePath);

                        WriteNLog.Write("File Created!");

                        break;

                    case string s when s.ToLower().StartsWith("hashpassword "):

                        string inputpassword = s.Substring("hashpassword ".Length);
                        WriteNLog.Write("Add Salt");
                        WriteNLog.Write("leave field blank for random Salt");
                        WriteNLog.Write();

                        command = Console.ReadLine();
                        WriteNLog.mainLog += command + Environment.NewLine;
                        string salt = command;

                        WriteNLog.Write("Passes?");
                        command = Console.ReadLine();
                        WriteNLog.mainLog += command + Environment.NewLine;

                        int passes = Convert.ToInt32(command);

                        WriteNLog.Write($"Hashed Passoword {Cryptography.SaltNHash(inputpassword, salt, passes)}");

                        break;

                    case string s when s.ToLower().StartsWith("mine "):
                        string mineHash = s.Substring("mine ".Length);
                        
                        WriteNLog.Write("Hash starting with?");
                        WriteNLog.Write();

                        command = Console.ReadLine();
                        string hashStartingWith = command;

                        Cryptography.Mine(mineHash, hashStartingWith);

                        break;

                    default:
                        WriteNLog.Write("Invalid command. Use help for list of commands");
                        WriteNLog.Write();
                        break;
                }
                command = Console.ReadLine();
                WriteNLog.mainLog += command + Environment.NewLine;
            }

            string ConsoleLine = String.Empty;
        
        }
        public static void CreateLog(string path)
        {
            string logFilename = $"ConsoleHashingApp{WriteNLog.RemoveInvalidFileCharacters(DateTime.Now.ToString())}";
            string logFilepath = Path.Combine(path, logFilename);

            while (File.Exists(logFilepath) == true)
            {
                int FileAmount = 1;
                logFilename += $"({FileAmount})";
                logFilepath = Path.Combine(path, logFilename);
                FileAmount++;
            }

            logFilepath += ".txt";

            FileStream stream = File.Create(logFilepath);

            stream.Dispose();
            stream.Close();

            File.WriteAllText(logFilepath, WriteNLog.mainLog);
        }
    }
    public static class WriteNLog
    {
        public static string mainLog = string.Empty;
        public static void Write(object contents)
        {
            Console.WriteLine(contents);

            mainLog += contents + Environment.NewLine;
        }
        public static void Write()
        {
            Console.WriteLine();
            mainLog += Environment.NewLine;
        }
        public static string RemoveInvalidFileCharacters(string Text)
        {
            List<char> invalidChars = new List<char>(Path.GetInvalidFileNameChars());
            char[] charstoTest = Text.ToCharArray();
            string outputChars = string.Empty;

            foreach (char c in charstoTest)
            {
                if (!invalidChars.Contains(c))
                {
                    outputChars += c;
                }
            }
            return outputChars;
        }
    }

    public static class Cryptography
    {
        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        public static string Mine(string toBeHashed, string hashStartingWith)
        {
            int nonce = 0;
            string hashThis = toBeHashed;

            for (bool i = hashThis.StartsWith(hashStartingWith); i == false; )
            {
                hashThis = toBeHashed;

                string mineThis = hashThis;

                mineThis += nonce;
                hashThis = Hash(mineThis);
                nonce++;

                Convert.ToString(nonce);   
            }
            string returnednonce = Convert.ToString(nonce);
            return returnednonce;
        }
        public static string SaltNHash(string password, string salt, int passes)
        {
            password += salt;
            for (int i = 0; i < passes; i++)
            {
                password = Cryptography.Hash(password);
                WriteNLog.Write(i);
            } 
            return password;
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
        public static string Hashfile(string Filepath)
        {
            if (Directory.Exists(Filepath))
            {
                WriteNLog.Write("Error: Filepath is a directory");
                WriteNLog.Write("");
                return null;
            }
            try
            {
                byte[] Filebytes = File.ReadAllBytes(Filepath);

                using (SHA256 mySHA256 = SHA256.Create())
                {
                    byte[] hashValue = mySHA256.ComputeHash(Filebytes);
                    return ByteArrayToString(hashValue);
                }
            }
            catch
            {
            }
            return null;
        }
        public static string GeneratePublicKey(string PrivateKey)
        {

            System.Security.Cryptography.AsymmetricAlgorithm.Create();

            return null;
        }      
    }
}