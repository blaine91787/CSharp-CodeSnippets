/*
 * Author:  Blaine Harris
 * Date:    10/2019
 * 
 * HackerRank Problem:
 * https://www.hackerrank.com/challenges/2d-array/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=arrays
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.InterviewPrep.Arrays
{
    public class Array2D_DS
    {
        public static int HourglassSum(int[][] arr)
        {
            // constraint 0 <= i,j <= 5
            int imax = 5;
            int jmax = 5;

            int tempMax;
            int max = int.MinValue;

            for (int i = 0; i <= imax - 2; i++)
            {
                for (int j = 0; j <= jmax - 2; j++)
                {
                    tempMax = 0;

                    tempMax += arr[i][j]; // topleft
                    tempMax += arr[i][j + 1]; // topmid
                    tempMax += arr[i][j + 2]; // topright
                    tempMax += arr[i + 1][j + 1]; // pivot
                    tempMax += arr[i + 2][j]; // botleft
                    tempMax += arr[i + 2][j + 1]; // botmid
                    tempMax += arr[i + 2][j + 2]; // botright

                    if (tempMax > max) max = tempMax;
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
