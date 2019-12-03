using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Mathematics.Fundamentals
{
    public class FindThePoint
    {
        /*
         * 
         * Returns two integers representing the 180-degree rotation
         * of point p across point q
         * 
         */
        static int[] findPoint(int px, int py, int qx, int qy)
        {
            int rx = (qx - px) + qx;
            int ry = (qy - py) + qy;

            return new int[] { rx, ry };
        }

        public static void Execute()
        {
            List<Test> tests = new List<Test>
            {
                new Test { px = 0, py = 0, qx = 1, qy = 1, ans = new int[] { 2, 2 } },
                new Test { px = 1, py = 1, qx = 2, qy = 2, ans = new int[] { 3, 3 } },
                new Test { px = 4, py = 3, qx = 5, qy = 2, ans = new int[] { 6, 1 } },
                new Test { px = 2, py = 4, qx = 5, qy = 6, ans = new int[] { 8, 8 } },
                new Test { px = 1, py = 2, qx = 2, qy = 2, ans = new int[] { 3, 2 } }
            };

            foreach (var test in tests)
            {
                int[] res = findPoint(test.px, test.py, test.qx, test.qy);

                if (res[0] == test.ans[0] && res[1] == test.ans[1]) Console.WriteLine("correct");
                else Console.WriteLine("incorrect");
            }
        }

        internal class Test
        {
            internal int px { get; set; }
            internal int py { get; set; }
            internal int qx { get; set; }
            internal int qy { get; set; }
            internal int[] ans { get; set; }
        }
    }
}
