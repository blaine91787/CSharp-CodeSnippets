/*
 * 
 * Author:          Blaine Harris
 * Date:            10/21/2019 
 * Description:     A class to encapsulate Directional Statistics.
 * Purpose:         Created for use in Phase Angle calculation for RBSPICE
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_CodeSnippets.Math
{
    public class DirectionalStatistics
    {
        public enum Units
        {
            Radians,
            Degrees
        }
        public enum TrigFunctions
        {
            Cosine,
            Sine,
            Tangent
        }

        /// <summary>
        /// Calculates the mean of the angles provided.
        /// </summary>
        /// <param name="angles">Enumerable of angles between 0-360</param>
        /// <param name="units">Units of the angles in radians or degrees</param>
        /// <returns></returns>
        public Double MeanOfAngles(IEnumerable<Double> angles, Units units = Units.Radians)
        {
            // Formula: alphaMean = Atan2 ( (1/n) Sum(cos(alpha), (1/n) Sum(sin(alpha)))
            // Link: https://en.wikipedia.org/wiki/Mean_of_circular_quantities


            // Sum the sines and cosines
            Double sinesSum = GetComponents(angles.ToArray(), TrigFunctions.Sine, units).Sum();
            Double cosinesSum = GetComponents(angles.ToArray(), TrigFunctions.Cosine, units).Sum();

            Int32 n = angles.Count();

            // Get the means of sines and cosines
            Double sbar = sinesSum / n;
            Double cbar = cosinesSum / n;

            // We now have all components of the formula

            Double alphaMean = System.Math.Atan2(sbar, cbar);

            // If angle is in qudrants 3-4 alphaMean will be negative
            if (alphaMean < 0) alphaMean = (2 * System.Math.PI) + alphaMean;

            // If the original units were degrees we need to convert back to degrees
            if (units == Units.Degrees) alphaMean = Rad2Deg(alphaMean);

            return alphaMean;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="angles"></param>
        /// <param name="func"></param>
        /// <param name="units"></param>
        /// <returns></returns>
        private Double[] GetComponents(Double[] angles, TrigFunctions func, Units units = Units.Radians)
        {
            Double[] comps = new Double[angles.Count()];

            if (units == Units.Degrees) angles = Deg2Rad(angles);

            for (int i = 0; i < angles.Length; i++)
            {
                if (func == TrigFunctions.Cosine) comps[i] = System.Math.Cos(angles[i]);
                else if (func == TrigFunctions.Sine) comps[i] = System.Math.Sin(angles[i]);
                else if (func == TrigFunctions.Tangent) comps[i] = System.Math.Tan(angles[i]);
                else throw new InvalidOperationException();
            }

            return comps;
        }

        private Double[] Rad2Deg(Double[] thetas)
        {
            for (int i = 0; i < thetas.Length; i++)
            {
                thetas[i] = thetas[i] * (180 / System.Math.PI);
            }
            return thetas;
        }   
        
        private Double Rad2Deg(double theta)
        {
            return theta * (180 / System.Math.PI);
        }

        private Double[] Deg2Rad(Double[] thetas)
        {
            for (int i = 0; i < thetas.Length; i++)
            {
                thetas[i] = thetas[i] * (System.Math.PI / 180);
            }
            return thetas;
        }

        private Double Deg2Rad(double theta)
        {
            return theta * (System.Math.PI / 180);
        }

        public static void ExecuteTests()
        {
            var test = new DirectionalStatistics();

            //List<Double> angles = new List<Double>
            //{
            //    341.213146154407,
            //    344.910730480917,
            //    353.17068473134,
            //    79.9573801299146,
            //    147.975669444772,
            //    155.372177151752
            //};

            List<Double> angles = new List<Double>
            {
                180,
                360
            };

            test.MeanOfAngles(angles, Units.Degrees);
        }
    }
}
