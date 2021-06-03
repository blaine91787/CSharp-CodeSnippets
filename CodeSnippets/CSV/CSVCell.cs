using System;
using System.Collections.Generic;
using System.Text;

namespace CSV
{
    public class CSVCell
    {
        private object _obj = null;
        private Type WindowsType = typeof(Object);
        public CSVCell(Object obj)
        {
            _obj = obj;
        }

        private Boolean DetermineType(Object obj)
        {
            if (obj is System.ValueType)
            {
                if (obj is System.Enum)
                {

                }
            }
            return false;
        }
    }
}
