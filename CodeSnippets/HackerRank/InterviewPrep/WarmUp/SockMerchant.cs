/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * Difficulty: Easy
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/sock-merchant/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=warmup
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.WarmUp
{
    public class SockMerchant
    {
        static bool numExists(int num, int[] ar)
        {
            foreach (int x in ar)
            {
                if (x == num) return true;
            }
            return false;
        }

        // Complete the sockMerchant function below.
        static int sockMerchant(int n, int[] ar)
        {
            int[] numsInAr = new int[n];
            int totalPairs = 0;
            for (int i = 0; i < n; i++)
            {
                if (!numExists(ar[i], numsInAr))
                {
                    numsInAr[i] = ar[i];
                    int colorTotal = 0;
                    int colorNum = ar[i];
                    int colorPair = colorNum * 2;
                    for (int j = 0; j < n; j++)
                    {
                        if (ar[j] == colorNum)
                        {
                            colorTotal += ar[j];
                        }
                    }
                    totalPairs += (colorTotal / colorPair);
                }
            }
            return totalPairs;
        }

        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
