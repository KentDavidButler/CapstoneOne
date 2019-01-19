using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapstoneOne
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                string[] word = ValidWord();
                Console.WriteLine("Welcome to the Pig Latin Translator!");
                Console.WriteLine("  ");
                word = PigLatin(word);
                Console.WriteLine(word.ToString());
                Console.WriteLine(" ");
                Console.Write("Would you like to run Pig Latin Translator again? ");
            } while (YesOrNo());


        }

        private static string[] PigLatin(string[] word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                int firstVowel = VowelLocation(word[i]);
                if (firstVowel == 0)
                {
                    word[i] = word[i] + "way";
                }
                else
                {
                    string wordManip = word[i].ToLower();
                    char[] stringArray = wordManip.ToCharArray();
                    word[i] = wordManip.Substring(firstVowel) + wordManip.Substring(0, firstVowel) + "ay";
                }
            }
            return word;
        }

        private static bool HasNonLetter(string word)
        {
            return word.All(Char.IsLetter);
        }

        private static int VowelLocation(string word)
        {
            char[] vowels = new char[] {'a', 'e', 'i','o','u' };
            char[] stringArray = word.ToCharArray();
            int location = 0;
            for (int i = 0; i < stringArray.Length; i++)
            {
                for (int j = 0; j < vowels.Length; j++)
                {
                    if(stringArray[i] == vowels[j])
                    {
                        return location;
                    }
                }
                location++;
            }
            return -1; //something went wrong
        }
        public static string[] ValidWord()
        {
            bool validInput;
            string[] strArray;
            String input;
            int wordAmmount;

            Console.Write("Enter a statement or word to be Translated: ");
            input = Console.ReadLine();
            strArray = input.Split(' ');
            wordAmmount = strArray.Count();

            for (int i = 0; i < wordAmmount; i++)
            {
                validInput = HasNonLetter(strArray[i]);
                if (!validInput)
                {
                    Console.Write("Please type a word without special characters or numbers: ");
                    break;
                }
            }
            return strArray;

        }

        private static bool YesOrNo()
        {
            string input;
            bool correctRespone = true;
            while (correctRespone)
            {
                Console.Write("(y/n):");
                input = Console.ReadLine().ToLower();
                if (String.Equals("n", input))
                {
                    return false;
                }
                else if (String.Equals("y", input))
                {
                    return true;
                }
                else
                {
                    Console.WriteLine("That is an invalid entry!");
                    continue;
                }
            }
            return false; //should never get hit needed to make it happy.
        }

    }
}
