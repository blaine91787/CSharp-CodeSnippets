/*
 * Author:  Blaine Harris
 * Date:    10/22/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/two-strings/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=dictionaries-hashmaps
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Random;

namespace HackerRank.InterviewPrep.DictionariesAndHashmaps
{
    public class TwoStrings
    {
        public static String twoStrings(String s1, String s2)
        {
            Dictionary<Char, Int16> dict = new Dictionary<Char, Int16>();
            Boolean found = false;

            for (int i = 0; i < s1.Length; i++)
            {
                if (!dict.ContainsKey(s1[i])) dict.Add(s1[i], 1);
            }

            for (int i = 0; i < s2.Length; i++)
            {
                if (dict.ContainsKey(s2[i]))
                {
                    found = true;
                    break;
                }
            }
            return found ? "YES" : "NO";
        }

        public static void Execute()
        {
            Int32 len = 27;
            RandomString rs = new RandomString(1);
            String s1 = rs.Next(len);
            String s2 = rs.Next(len);

            Console.WriteLine(twoStrings(s1, s2));

            Console.ReadKey();
        }
    }
}
