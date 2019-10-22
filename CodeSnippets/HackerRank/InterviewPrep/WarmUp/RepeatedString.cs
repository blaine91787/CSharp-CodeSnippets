/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * Difficulty: Easy
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/repeated-string/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=warmup
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.WarmUp
{
    class RepeatedString
    {
        static long repeatedString(string s, long n)
        {
            long total = 0;

            long AsInSubString = total;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'a') AsInSubString++;
            }

            long numSubStrings = n / s.Length;

            long remainder = n % s.Length;

            total = numSubStrings * AsInSubString;

            for (int i = 0; i < remainder; i++)
            {
                if (s[i] == 'a') total++;
            }

            return total;
        }

        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
