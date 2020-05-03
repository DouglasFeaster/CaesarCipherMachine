using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipherMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            char[] alphabet = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (Char)i).ToArray();

            string output = "";
            string quit = "";
            string keyText = "";
            int key = 0;

            while (quit == String.Empty)
            {
                keyText = "";
                key = 0;

                Console.WriteLine("[1] Encode");
                Console.WriteLine("[2] Decode");

                Console.Write(": ");

                string modeText = Console.ReadLine().ToLower();
                            
                if (Int32.TryParse(modeText, out int mode))
                {
                    switch (mode)
                    {
                        case 1:
                            Console.Write("Key: ");
                            keyText = Console.ReadLine();
                            if (Int32.TryParse(keyText, out key) )
                            {
                                output = String.Empty;
                                Console.Write("Plain Text: ");
                                string plainText = Console.ReadLine().ToLower();
                                output = Encode(alphabet, plainText, key);
                                Console.WriteLine(output);

                                Console.WriteLine("[1] Save to File");
                                Console.Write(": ");
                                string saveToFile = Console.ReadLine();

                                if (saveToFile == "1")
                                {
                                    Console.Write("File Path: ");
                                    string path = Console.ReadLine();
                                    SaveToFile(path, output);
                                }

                                Console.WriteLine();
                            }
                                

                            break;

                        case 2:
                            Console.Write("Key: ");
                            keyText = Console.ReadLine();
                            if (Int32.TryParse(keyText, out key))
                            {
                                output = String.Empty;
                                string cipherText = String.Empty;

                                Console.WriteLine("[1] Read from Console");
                                Console.WriteLine("[2] Read from File");

                                Console.Write(": ");
                                string choice = Console.ReadLine();

                                if (choice == "1")
                                {
                                    Console.Write("Cipher Text: ");
                                    cipherText = Console.ReadLine().ToLower();
                                }
                                else if (choice == "2")
                                {
                                    Console.Write("File Path: ");
                                    string path = Console.ReadLine();
                                    cipherText = ReadFromFile(path);
                                }

                                output = Decode(alphabet, cipherText, key);
                                Console.WriteLine(output);

                                Console.WriteLine("[1] Save to File");
                                Console.Write(": ");
                                string saveToFile = Console.ReadLine();

                                if (saveToFile == "1")
                                {
                                    Console.Write("File Path: ");
                                    string path = Console.ReadLine();
                                    SaveToFile(path, output);
                                }

                                Console.WriteLine();
                            }
                            break;

                        default:
                            Console.WriteLine("Press any key to Quit");
                            quit = Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Press any key to Quit");
                    quit = Console.ReadKey().KeyChar.ToString();
                }
            }

        }

        private static string Cipher(char[] alphabet, string plainText, int key)
        {
            string output = "";
            
            foreach (char letter in plainText)
            {
                if (alphabet.Contains(letter))
                {
                    int a = Array.IndexOf(alphabet, letter);

                    int b = ((((a + key) - 0) % 26) + 0);
                    output += alphabet[b];
                }
                else
                {
                    output += " ";
                }
            }

            return output;
        }

        private static string Encode(char[] alphabet, string plainText, int key)
        {
            return Cipher(alphabet, plainText, key);
        }

        private static string Decode(char[] alphabet, string plainText, int key)
        {
            return Cipher(alphabet, plainText, 26 - key);
        }

        private static void SaveToFile(string path, string output)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.Write(output);
            }
        }

        private static string ReadFromFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
               return streamReader.ReadToEnd();
            }
        }
    }
}
