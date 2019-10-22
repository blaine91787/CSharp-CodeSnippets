/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/new-year-chaos/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=arrays
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.Arrays
{
    public class NewYearChaos
    {
        public static void MinimumBribes(int[] q)
        {
            int ans = 0;
            for (int i = q.Length - 1; i >= 0; i--)
            {
                if (q[i] - (i + 1) > 2)
                {
                    Console.WriteLine("Too chaotic");
                    return;
                }

                for (int j = Math.Max(0, q[i] - 2); j < i; j++)
                    if (q[j] > q[i]) ans++;
            }
            Console.WriteLine(ans);
        }

        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
