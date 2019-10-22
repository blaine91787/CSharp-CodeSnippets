/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * Difficulty: Easy
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/jumping-on-the-clouds/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=warmup
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.WarmUp
{
    public class JumpingOnTheClouds
    {
        // Complete the jumpingOnClouds function below.
        static int jumpingOnClouds(int[] c)
        {
            if (c.Length == 2) return 1;

            Stack<int> s = new Stack<int>();

            int i = 0;
            while (i < c.Length - 1)
            {
                if ((i + 2) < c.Length)
                {
                    if (c[i + 2] == 0)
                    {
                        s.Push(i += 2);
                    }
                    else if (c[i + 1] == 0)
                    {
                        s.Push(i += 1);
                    }
                }
                else if ((i + 1) == c.Length - 1)
                {
                    s.Push(i += 1);
                }
            }
            return s.Count();
        }

        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
