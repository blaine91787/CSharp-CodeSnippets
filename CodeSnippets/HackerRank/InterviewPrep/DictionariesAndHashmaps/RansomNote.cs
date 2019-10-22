/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/ctci-ransom-note/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=dictionaries-hashmaps
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.DictionariesAndHashmaps
{
    public class RansomNote
    {
        static void checkMagazine(string[] magazine, string[] note)
        {
            Dictionary<String, Int32> dict = new Dictionary<String, Int32>();

            for (int i = 0; i < magazine.Length; i++)
            {
                if (!dict.ContainsKey(magazine[i]))
                {
                    dict.Add(magazine[i], 1);
                }
                else
                {
                    dict[magazine[i]] += 1;
                }
            }


            for (int i = 0; i < note.Length; i++)
            {
                if (!dict.ContainsKey(note[i]))
                {
                    Console.WriteLine("No");
                    return;
                }
                else if (dict[note[i]] > 0)
                {
                    dict[note[i]] -= 1;
                }
                else if (dict[note[i]] == 0)
                {
                    Console.WriteLine("No");
                    return;
                }
            }
            Console.WriteLine("Yes");
        }

        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
