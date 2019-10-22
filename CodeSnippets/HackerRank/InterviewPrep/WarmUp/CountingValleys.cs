/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * Difficulty: Easy
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/counting-valleys/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=warmup
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.WarmUp
{
    public class CountingValleys
    {
        static int countingValleys(int n, string s)
        {
            int total = 0; // sealevel
            int prevTotal = 0;
            int totalValleys = 0;
            int totalMountains = 0;
            bool aboveSeaLevel = false;
            bool belowSeaLevel = false;

            foreach (char c in s)
            {
                prevTotal = total;
                if (c == 'D') // downhill
                {
                    total--;
                    if (total == 0 && belowSeaLevel)
                    {
                        totalValleys++;
                        belowSeaLevel = false;
                    }
                    else if (total == 0 && aboveSeaLevel)
                    {
                        aboveSeaLevel = false;
                        totalMountains++;
                    }

                    if (prevTotal == 0) belowSeaLevel = true;
                }
                else // uphill
                {
                    total++;
                    if (total == 0 && aboveSeaLevel)
                    {
                        totalMountains++;
                        aboveSeaLevel = false;
                    }
                    else if (total == 0 && belowSeaLevel)
                    {
                        belowSeaLevel = false;
                        totalValleys++;
                    }

                    if (prevTotal == 0) aboveSeaLevel = true;
                }
            }
            if (total != 0) throw new Exception("Total should be 0");
            if (belowSeaLevel) totalValleys++;
            return totalValleys;
        }

        public static void Execute()
        {
            throw new NotImplementedException();
        }
    }
}
