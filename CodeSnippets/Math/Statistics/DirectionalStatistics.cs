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

namespace Math.Statistics
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
        /// Calculates the mean of an array of angles in degrees or radians.
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

            // If angle is in qudrants 3-4 Atan2 will return a negative, so we translate
            if (alphaMean < 0) alphaMean = (2 * System.Math.PI) + alphaMean;

            // If the original units were degrees we need to convert back to degrees
            if (units == Units.Degrees) alphaMean = Rad2Deg(alphaMean);

            return alphaMean;
        }

        /// <summary>
        /// Converts an array of angles in degrees or radians into an array of the sines, cosines, or tangents based on the func parameter.
        /// </summary>
        /// <param name="angles"></param>
        /// <param name="func"></param>
        /// <param name="units"></param>
        /// <returns>Depending on func parameter will return an array of sines, cosines, or tangents.</returns>
        /// <exception cref="InvalidOperationException"></exception>
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

        private Double Rad2Deg(Double theta)
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

        private Double Deg2Rad(Double theta)
        {
            return theta * (System.Math.PI / 180);
        }

        public static void Execute()
        {
            var test = new DirectionalStatistics();

            List<Double> angles = new List<Double>
            {
                341.213146154407,
                344.910730480917,
                353.17068473134,
                79.9573801299146,
                147.975669444772,
                155.372177151752
            };

            string st = "[" + angles[0] + ", " + angles[1] + ", " + angles[2] + ", " + angles[3] + ", " + angles[4] + ", " + angles[5] + "]";

            Console.WriteLine("Mean of " + st + " = " + test.MeanOfAngles(angles, Units.Degrees));


            angles = new List<Double>
            {
                0,
                90
            };

            Console.WriteLine("Mean of [0, 90] = " + test.MeanOfAngles(angles, Units.Degrees));

            angles = new List<Double>
            {
                90,
                180
            };

            Console.WriteLine("Mean of [90, 180] = " + test.MeanOfAngles(angles, Units.Degrees));


            angles = new List<Double>
            {
                180,
                270
            };

            Console.WriteLine("Mean of [180, 270] = " + test.MeanOfAngles(angles, Units.Degrees));

            angles = new List<Double>
            {
                270,
                360
            };

            Console.WriteLine("Mean of [180, 360] = " + test.MeanOfAngles(angles, Units.Degrees));

            angles = new List<Double>
            {
                .01,
                360
            };

            Console.WriteLine("Mean of [0.01, 360] = " + test.MeanOfAngles(angles, Units.Degrees));

            angles = new List<Double>
            {
                45,
                315
            };

            Console.WriteLine("Mean of [45, 315] = " + test.MeanOfAngles(angles, Units.Degrees));

            angles = new List<Double>
            {
                0,
                90,
                180,
                270
            };

            st = "[" + angles[0] + ", " + angles[1] + ", " + angles[2] + ", " + angles[3] + "]";

            Console.WriteLine("Mean of " + st + " = " + test.MeanOfAngles(angles, Units.Degrees));
        }
    }
}
