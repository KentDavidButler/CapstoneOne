/*Written by Kent Butler
 *Written between 1/18 and 1/19
 * 
 * The following program is a pig latin translator.The program will translate any word that consists 
 * of characters only. If the word has any characters or punctuation at the beginning or within it,
 * the word will not get translated. The following punctuation will remain in its correct location
 * within the sentance or sentances: ',' '.' '!' '?'.
 * 
 * This program will also keep capitalization in the translated word same character space. For example, 
 * SoMeThInG would become OmEtHiNgSay. To further explain, it checks X character at Location Y within
 * the word and if that character at location Y is capital, the program will ensure that the translated
 * word has a capital letter at location Y.
 * 
 */

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

                Console.Write("Please type a word, a sentance, or sentances to translate: ");
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


        private static string[] StringSplit(string input, out bool empty)
        {
            string[] words = input.Split(' ');
            words = words.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //removes null and empty locations in array (for the most part)
            if(words.Length==0)
            {
                empty = true;
                return words;
            }
            empty = false;
            return words;
        }

        private static string[] PigLatin(string[] word)
        {
            for (int i = 0; i < word.Length; i++)
            {
                bool[] isCapital = getCapitalLetterLoc(word[i].ToCharArray()) ;

                int firstVowel = VowelLocation(word[i].ToLower());
                if (HasNonLetter(word[i]) && !IsContraction(word[i]))
                {
                    //we need to check if the non letter is punctuation and if so keep it at end of word
                    if (HasProperPunctuation(word[i]))
                    {
                        word[i] = PigLatinPunc(word[i], firstVowel);
                    }
                    else
                    {
                        word[i] = word[i];
                    }
                   
                }
                else if (firstVowel == 0) 
                {
                    //covers words with vowels as first letters and words without vowels such as "by"
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

        private static string PigLatinPunc(string word, int firstVowel)
        {
            char last = word[word.Length - 1];
            word = word.TrimEnd(last);

            if (HasNonLetter(word))
            {
                //do nothing
                //This covers words that have punctuation at the end and have a non letter in it.
                //I ran into this issue when dealing with email addresses at the end of sentances.
            }
            else if (string.IsNullOrEmpty(word))
            {
                //do nothing
                //I ran into a situation when typing in a word followed by "..." and space would cause
                //a run time error. This covers that issue.
            }
            else if (firstVowel == 0)
            {
                word = word + "way";
            }
            else
            {
                string wordManip = word;
                char[] stringArray = wordManip.ToCharArray();
                word = wordManip.Substring(firstVowel) + wordManip.Substring(0, firstVowel) + "ay";
            }

            return word + last + "";
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
            return 0; //no vowels
        }

        private static bool[] getCapitalLetterLoc(char[] charArray)
        {
            //snags the capital letter location of a string and returns it to a bool array
            char[] localArray = new char[charArray.Length];
            Array.Copy(charArray, localArray, localArray.Length);

            bool[] isUpper = new bool[localArray.Length];
            char temp;
            for (int i = 0; i < localArray.Length; i++)
            {
                temp = localArray[i];
                if (Char.IsLetter(temp)) //validate that input is a character
                {
                    isUpper[i] = char.IsUpper(localArray[i]);
                }
                else
                {
                    isUpper[i] = false;
                }
            }
            return isUpper;
        }

        private static bool HasNonLetter(string word)
        {
            //possibly un-needed but cleaned up code when using
            return !(word.All(Char.IsLetter));
        }

        private static bool IsContraction(string word)
        {
            //Method to check for the "don't, can't, ect." of the english language
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

        private static bool HasProperPunctuation(string word)
        {
            char[] punctuation = new char[] { '.', '?', '!', ','};
            char last = word[word.Length - 1];

            for (int i = 0; i < punctuation.Length; i++)
            {
                if(last == punctuation[i])
                {
                    return true;
                }
            }
            return false;
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
