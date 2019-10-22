using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Random
{
    public class RandomString
    {
        private static System.Random _rand = new System.Random();
        public Int32 StringSize { get; set; } = Int32.MinValue;
        public RandomString(Int32 seed = Int32.MinValue)
        {
            if (seed > 0) _rand = new System.Random(seed);
        }
        public String Next(Int32 length = 5, Int32 seed = Int32.MinValue)
        {
            if (length > 0) StringSize = length;
            if (StringSize <= 0) return null;
            Char[] charArray = new char[StringSize];
            if (charArray == null) throw new Exception();

            if (seed > 0) _rand = new System.Random(seed);

            for (int i = 0; i < charArray.Length; i++)
            {
                charArray[i] = (Char)(_rand.Next(97, 123));
            }

            return new string(charArray);
        }
    }
}
