/*
 * Author:  Blaine Harris
 * Date:    10/24/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/sherlock-and-anagrams/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=dictionaries-hashmaps
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.DictionariesAndHashmaps
{
    public class SherlockAndAnagrams
    {
        /*
         * 
         * Returns the number of anagrammatic substrings in s
         * 
         */
        static int sherlockAnagrams(string s)
        {
            var substrings = new Dictionary<string, int>();

            for (int i = 0; i < s.Length; i++)
            {
                string tempstring = "";
                for (int j = i; j < s.Length; j++)
                {
                    tempstring += s[j];
                    tempstring = String.Concat(tempstring.OrderBy(c => c));
                    if (!substrings.ContainsKey(tempstring))
                    {
                        substrings.Add(tempstring, 1);
                    }
                    else
                    {
                        substrings[tempstring]++;
                    }
                }
            }

            int anagrams = 0;
            foreach (var substring in substrings)
            {
                int count = substring.Value;
                if (count > 1)
                {
                    int n = count;
                    anagrams += (n * (n - 1)) / 2;
                }
            }

            return anagrams;
        }

        public static void Execute()
        {
            Dictionary<string, int> tests = new Dictionary<string, int>
            {
                { "abba", 4 },
                { "abcd", 0 },
                { "ifailuhkqq", 3 },
                { "kkkk", 10 },
                { "cdcd", 5 },
                { "ifailuhkqqhucpoltgtyovarjsnrbfpvmupwjjjfiwwhrlkpekxxnebfrwibylcvkfealgonjkzwlyfhhkefuvgndgdnbelgruel", 399 },
                { "gffryqktmwocejbxfidpjfgrrkpowoxwggxaknmltjcpazgtnakcfcogzatyskqjyorcftwxjrtgayvllutrjxpbzggjxbmxpnde", 471 },
                { "mqmtjwxaaaxklheghvqcyhaaegtlyntxmoluqlzvuzgkwhkkfpwarkckansgabfclzgnumdrojexnrdunivxqjzfbzsodycnsnmw", 370 },
                { "ofeqjnqnxwidhbuxxhfwargwkikjqwyghpsygjxyrarcoacwnhxyqlrviikfuiuotifznqmzpjrxycnqktkryutpqvbgbgthfges", 403 },
                { "zjekimenscyiamnwlpxytkndjsygifmqlqibxxqlauxamfviftquntvkwppxrzuncyenacfivtigvfsadtlytzymuwvpntngkyhw", 428 }
            };
            
            foreach (var kv in tests)
            {
                Console.WriteLine(sherlockAnagrams(kv.Key) == kv.Value ? "correct" : "incorrect");
            }
        }
    }
}
