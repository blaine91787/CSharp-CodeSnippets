using Garbage;
using HackerRank.InterviewPrep.DictionariesAndHashmaps;
using HackerRank.Mathematics.Fundamentals;
using Math.Statistics;
using PerformanceTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var file1 = @"C:\Users\blaine.harris\Downloads\ubuntu-18.04.3-desktop-amd64.iso"; //"\\ftecs.com\data\Testing\RBSPICE\Data_Root\Production\RBSP\RBSPB\RBSPICE\Data\Level_3\TOFxEnonH\2015\rbsp-b-rbspice_lev-3_TOFxEnonH_20150317_v1.1.12-07.cdf";
            var file2 = @"C:\Users\blaine.harris\Desktop\ubuntu-18.04.3-desktop-amd64.iso";//C:\Blaine_Temp\rbsp-b-rbspice_lev-3_TOFxEnonH_20150317_v1.1.12-07.cdf";

            try
            {
                Utilities.Cryptography.HashComparer comp = new Utilities.Cryptography.HashComparer();
                Console.WriteLine("File1.Name = " + comp.File1.Fileinfo.Name);
                Console.WriteLine("File1.Hash == " + comp.File1.HashString);
                Console.WriteLine("File2.Name = " + comp.File2.Fileinfo.Name);
                Console.WriteLine("File2.Hash == " + comp.File2.HashString);
                Console.WriteLine("AreEqual == " + comp.AreEqual());

                Console.WriteLine();

                comp.AreEqual(file1, file2, "sha256");
                Console.WriteLine("File1.Name = " + comp.File1.Fileinfo.Name);
                Console.WriteLine("File1.Hash == " + comp.File1.HashString);
                Console.WriteLine("File2.Name = " + comp.File2.Fileinfo.Name);
                Console.WriteLine("File2.Hash == " + comp.File2.HashString);
                Console.WriteLine("AreEqual == " + comp.AreEqual());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            Console.ReadKey();
        }
    }
}
