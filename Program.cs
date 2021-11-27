using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleHashingApp
{


    class Program
    {
        public static FileStream fileStreamLog;

        public static string mainlog = string.Empty;
        public static void Main(string[] args)
        {

            //fileStreamLog = File.OpenWrite(logPath);





            

            writeNlog(DateTime.Now);

            String Version = "Version Alpha 0.03";
            //for (int rounds = 0; rounds <= 10;)
            //{
            //    Console.Beep(960, 100);
            //    Console.Beep(853, 100);
            //    rounds++;
            //}
            

            writeNlog("+------------------------------------------------+");
            writeNlog("|   Welcome to ConsoleHashingApp                 |");
            writeNlog("|   Made by Lunar HUE                            |");
            writeNlog("|   LunarHUE.com                                 |");
            writeNlog("|                                                |");
            writeNlog($"|   {Version}                           |");
            writeNlog("+------------------------------------------------+");
            writeNlog("Enter command. Use help for a list of commands");

            writeNlog();

            String Command = Console.ReadLine().ToLower();

            while (!Command.ToLower().Equals("quit"))
            {
                switch (Command)
                {
                    case string s when s.ToLower().StartsWith("help"):
                        writeNlog("");
                        writeNlog("Command     | Syntax                 | Usage");
                        writeNlog("------------+------------------------+-------------------------------------------------");
                        writeNlog("Hash        | Hash (plaintext)       | Plaintext into a Hash using SHA256");
                        writeNlog("Quit        | Quit                   | Quit the program");
                        writeNlog("Hashfile    | Hashfile (Directory)   | Files into a Hash using SHA256");
                        writeNlog("OutputFile  | OutputFile (Directory) | Output the commands and outputs of this instance");
                        writeNlog();
                        writeNlog("Commands are NOT case sensitive");
                        writeNlog();
                        break;

                    case string s when s.ToLower().StartsWith("hash "):
                        string output = Hash(Command.Substring("hash ".Length));

                        writeNlog($"Hash: {output}");
                        writeNlog();

                        break;

                    case string s when s.ToLower().StartsWith("hashfile "):
                        string hashFilePath = s.Substring("hashfile ".Length);

                        string Hashedfile = Hashfile(hashFilePath);
                        if(Hashedfile != null)
                        {
                            writeNlog($"Hashed File: {Hashfile(hashFilePath)}");
                            writeNlog();
                        }
                        else
                        {
                            writeNlog("Check Documentation");
                            writeNlog();
                        }

                        break;
                        

                    case string s when s.ToLower().StartsWith("outputfile "):
                        string logFilePath = s.Substring("outputfile ".Length);
                        createlog(logFilePath);

                        writeNlog("File Created!");

                        break;

                    default:
                        writeNlog("Invalid command. Use help for list of commands");
                        writeNlog();
                        break;
                }
                Command = Console.ReadLine();
                mainlog += Command + Environment.NewLine;
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
        public static string Hashfile(string Filepath)
        {
            if (Directory.Exists(Filepath))
            {
                writeNlog("Error: Filepath is a directory");
                writeNlog("");
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

        public static void writeNlog(object contents)
        {
            Console.WriteLine(contents);

            mainlog += contents + Environment.NewLine;
            //Console.WriteLine(mainlog);
        }

        public static void writeNlog()
        {
            Console.WriteLine();
            mainlog += Environment.NewLine;
        }

        public static void createlog(string path)
        {
            string logFilename = $"ConsoleHashingApp{RemoveInvalidCharacters("xxxx2021-11-27 12:56:40 AM")}";
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

            File.WriteAllText(logFilepath, mainlog);
        }
        public static string RemoveInvalidCharacters(string Text)
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

}
