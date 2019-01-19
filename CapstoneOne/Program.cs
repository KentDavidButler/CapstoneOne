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
            Console.WriteLine("Welcome to the Pig Latin Translator!");
            do
            {
                string[] words;
                string input, output;

                Console.Write("Please type a word or sentance to translate: ");
                input = Console.ReadLine();
                words = StringSplit(input, out bool empty);

                if (empty)
                {
                    Console.WriteLine("You need to enter something!");
                }
                else
                {
                    words = PigLatin(words);
                    output = String.Join(" ", words);
                    Console.WriteLine(output);
                }

                Console.WriteLine(" ");
                Console.Write("Would you like to run Pig Latin Translator again? ");
            } while (YesOrNo());

            Console.WriteLine(" ");
            Console.WriteLine("Goodbye!");
        }


        private static string[] PigLatin(string[] word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                bool[] isCapital = getCapitalLetterLoc(word[i].ToCharArray()) ;

                int firstVowel = VowelLocation(word[i].ToLower());
                if (HasNonLetter(word[i]) && !IsContraction(word[i]))
                {
                    word[i] = word[i];
                }
                else if (firstVowel == 0)
                {
                    word[i] = word[i] + "way";
                }
                else
                {
                    string wordManip = word[i];
                    char[] stringArray = wordManip.ToCharArray();
                    word[i] = wordManip.Substring(firstVowel) + wordManip.Substring(0, firstVowel) + "ay";
                }
                word[i] = PassCapitalLocation(word[i], isCapital) + "";
            }
            return word;
        }

        private static string PassCapitalLocation(string word, bool[] isCapital)
        {
            char[] tempChar = word.ToCharArray();
            for (int i = 0; i < isCapital.Length; i++)
            {
                if(isCapital[i])
                {
                    tempChar[i] = char.ToUpper(tempChar[i]);
                }
                else
                {
                    tempChar[i] = char.ToLower(tempChar[i]);
                }
            }
            string temp = string.Join("", tempChar);
            return temp;
        }

        private static bool[] getCapitalLetterLoc(char[] charArray)
        {
            char[] localArray = new char[charArray.Length];
            Array.Copy(charArray, localArray, localArray.Length);

            bool[] isUpper = new bool[localArray.Length];
            char temp;
            for (int i = 0; i < localArray.Length; i++)
            {
                temp = localArray[i];
                isUpper[i] = char.IsUpper(localArray[i]);
            }
            return isUpper;
        }

        private static string[] StringSplit(string input, out bool empty)
        {
            string[] words = input.Split(' ');
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //removes null and empty locations in array
            if(words.Length==0)
            {
                empty = true;
                return words;
            }
            empty = false;
            return words;
        }

        private static bool HasNonLetter(string word)
        {
            return !(word.All(Char.IsLetter));
        }

        private static bool IsContraction(string word)
        {
            char[] stringArray = word.ToCharArray();
            for (int i = 0; i < stringArray.Length; i++)
            {
                if(stringArray[i] == '\'')
                {
                    return true;
                }
            }

            return false;
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
