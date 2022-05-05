using System;

namespace PasswordGenerator
{
    internal class Program
    {
        static readonly Random rand = new Random();
        static void Main(string[] args)
        {
            

            if (!IsValid(args))
            {
                Console.WriteLine("Arguments are not valid");
                return;
            }

            string password = GenerateRandomPassword(Convert.ToInt32(args[0]), args[1]);
            Console.WriteLine(password);

        }
        private static bool IsValid(string[] args)
        {
            string validArgs = "Llds";

            if (!int.TryParse(args[0], out _))
            {
                return false;
            }

            if (args[1].Length > Convert.ToInt32(args[0]))
            {
                return false;
            }

            foreach (char c in args[1])
            {
                if (!validArgs.Contains(c))
                {
                    return false;
                }
            }
            return true;

        }

        private static string GenerateRandomPassword(int passwordLength, string options)
        {
            string[] password = new string[passwordLength];
            int?[] usedIndexes = new int?[passwordLength];
            for (int i = 0; i < passwordLength; i++)
            {
                password[i] = GenerateRandomLetter('a', 'z');
            }

            foreach (char c in options)
            {
                string character = "a";
                int randomIndex = rand.Next(0, passwordLength);

                while (usedIndexes[randomIndex] != null)
                {
                    randomIndex = rand.Next(0, passwordLength);
                }

                switch (c)
                {
                    case 'L':
                        {
                            character = GenerateRandomLetter('A', 'Z');
                            break;
                        }
                    case 'l':
                        {
                            character = GenerateRandomLetter('a', 'z');
                            break;
                        }
                    case 'd':
                        {
                            character = GenerateRandomDigit();
                            break;
                        }
                    case 's':
                        {
                            character = GenerateRandomSpecialSymbol();
                            break;
                        }
                    default:
                        break;
                }
                usedIndexes[randomIndex] = randomIndex; 
                password[randomIndex] = character;
            }

            return String.Concat(password); 
        }

        private static string GenerateRandomSpecialSymbol()
        {
            const string specialChars = "!\"#¤%&/(){}[]";
            int index = rand.Next(0, specialChars.Length + 1);
            return specialChars[index].ToString();
        }

        private static string GenerateRandomLetter(char min, char max)
        {
            string randomLetter = ((char)rand.Next(min, max)).ToString();
            
            return randomLetter;
        }

        private static string GenerateRandomDigit()
        {
            int randomDigit = rand.Next(0, 10);
            return randomDigit.ToString();
        }
    }
}
