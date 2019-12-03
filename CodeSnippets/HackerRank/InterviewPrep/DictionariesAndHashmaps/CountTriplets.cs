/*
 * Author:  Blaine Harris
 * Date:    10/24/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/count-triplets-1/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=dictionaries-hashmaps
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.DictionariesAndHashmaps
{
    public class CountTriplets
    {
        #region COMPLETES TESTS 0, 1, 2, 12
        public long countTriplets(List<long> arr, long r)
        {
            long count = 0;
            for (int i = 0; i < arr.Count(); i++)
            {
                for (int j = i+1; j < arr.Count(); j++)
                {
                    for (int k = j+1; k < arr.Count(); k++)
                    {
                        long[] triplet = new long[] { arr[i], arr[j], arr[k] };
                        if (IsGeoSeries(triplet, r)) count++;
                    }
                }
            }

            return count;
        }

        private bool IsGeoSeries(long[] triplet, long r)
        {
            for (long i = 0; i < long.MaxValue; i++)
            {
                long geo = (long)Math.Pow(r, i);
                if (geo > triplet[2]) break;
                if (triplet[0] == geo)
                {
                    long[] series = new long[] { geo, (long)Math.Pow(r, i + 1), (long)Math.Pow(r, i + 2) };
                    if (triplet[0] == series[0] && triplet[1] == series[1] && triplet[2] == series[2]) return true;
                }
            }
            return false;
        }

        //private long power(long r, long i)
        //{
        //    long total = r;
        //    if (i == 0) return 1;
        //    for (long j = 1; j < i; j++)
        //    {
        //        total *= r;
        //    }
        //    return total;
        //}
        #endregion

        public static void Execute()
        {
            var ct = new CountTriplets();

            List<Test> tests = new List<Test>()
            {
                new Test { r = 2, ans = 2, arr = new List<long>() { 1, 2, 2, 4 } },
                new Test { r = 3, ans = 6, arr = new List<long>() { 1, 3, 9, 9, 27, 81 } },
                new Test { r = 5, ans = 4, arr = new List<long>() { 1, 5, 5, 25, 125 } }
            };

            foreach (var test in tests)
            {
                Console.WriteLine(ct.countTriplets(test.arr, test.r) == test.ans ? "correct" : "incorrect");
            }
        }


        protected class Test
        {
            public long r { get; set; }
            public List<long> arr { get; set; }
            public long ans { get; set; } = long.MinValue;
        }
    }
}
