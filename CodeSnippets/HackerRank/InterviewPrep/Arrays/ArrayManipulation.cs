/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/crush/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=arrays
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.Arrays
{
    public class ArrayManipulation
    {
        public static long arrayManipulation(int n, int[][] queries)
        {
            long[] array = new long[n];
            long max = long.MinValue;
            int numQueries = queries.GetLength(0);

            for (int i = 0; i < numQueries; i++)
            {
                int a = queries[i][0] - 1;
                int b = queries[i][1] - 1;
                int k = queries[i][2];
                for (int j = 0; j < n; j++)
                {
                    if ((j >= a) && (j <= b))
                    {
                        array[j] += k;
                        if (array[j] > max)
                        {
                            max = array[j];
                        }
                    }
                }
            }

            return max;
        }
        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
