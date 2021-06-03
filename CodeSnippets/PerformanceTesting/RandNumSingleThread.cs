using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceTesting
{
    public class RandNumSingleThread
    {
        #region Parameters
        public Int32[] NumValues { get; set; } = new int[] { 10, 100, 1000, 10000, 100000, 1000000, 10000000 };
        #endregion Parameters

        public void Run()
        {
            Stopwatch sw = new Stopwatch();

            Console.WriteLine();
            Console.WriteLine("### Array Insert ###");
            Console.WriteLine();
            foreach (var num in NumValues)
            {
                Int32[] list = new Int32[num]; 
                Random rand = new Random(Seed: 12345);
                rand.Next();
                sw.Start();
                for (var i = 0; i < num; i++)
                {                            
                    list[i] = rand.Next(Int16.MinValue, Int16.MaxValue);
                }
                Console.WriteLine(num + " values took " + sw.ElapsedTicks);
                sw.Reset();
            }



            Console.WriteLine();
            Console.WriteLine("### List Insert ###");
            Console.WriteLine();
            foreach (var num in NumValues)
            {
                List<Int32> list = new List<Int32>();
                Random rand = new Random(Seed: 1);
                rand.Next();
                sw.Start();
                for (var i = 0; i < num; i++)
                {
                    list.Add(rand.Next(Int16.MinValue, Int16.MaxValue));
                }
                Console.WriteLine(num + " values took " + sw.ElapsedTicks);
                sw.Reset();
            }



            Console.WriteLine();
            Console.WriteLine("### Array Access ###");
            Console.WriteLine();
            foreach (var num in NumValues)
            {
                Int32[] list = new Int32[num];
                Random rand = new Random(Seed: 1);
                rand.Next();

                // Populate
                for (var i = 0; i < num; i++)
                {
                    list[i] = rand.Next(Int16.MinValue, Int16.MaxValue);
                }

                // Access
                sw.Start();
                for (var i = 0; i < num; i++)
                {
                    var x = list[rand.Next(0, num-1)];
                }
                Console.WriteLine(num + " values took " + sw.ElapsedTicks);
                sw.Reset();
            }

            Console.WriteLine();
            Console.WriteLine("### List Access ###");
            Console.WriteLine();
            foreach (var num in NumValues)
            {
                List<Int32> list = new List<Int32>();
                Random rand = new Random(Seed: 1);
                rand.Next(); 

                // Populate 
                for (var i = 0; i < num; i++)
                {
                    list.Add(rand.Next(Int16.MinValue, Int16.MaxValue));
                }

                // Access
                sw.Start();
                for (var i = 0; i < num; i++)
                {                   
                    var x = list[rand.Next(0, num - 1)];
                }
                Console.WriteLine(num + " values took " + sw.ElapsedTicks);
                sw.Reset();
            }
        }

        public static void Execute()
        {
            var rnsd = new RandNumSingleThread();
            rnsd.Run();
        }
    }
}
