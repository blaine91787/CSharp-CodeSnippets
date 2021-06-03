using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;

namespace CSV
{
    /// <summary>
    /// An abstraction of a CSV file.
    /// </summary>
    public class CSVFile
    {
        private System.IO.FileInfo _file = null;
        private String _filepath = String.Empty;
        private Dictionary<String, Int32> _headerIndex = null;
        private Object[][] _table = null;

        public String Path { get; set; } = String.Empty;

        /// <summary>
        /// The number of rows in the file, excluding the header row.
        /// </summary>
        public Int32 Length { get; set; } = -1;

        /// <summary>
        /// Constructor for the CSVFile class. Instantiates the class with basic CSV file information from the provided path.
        /// </summary>
        /// <param name="path"></param>
        public CSVFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                // Save file information
                _filepath = path;
                _file = new System.IO.FileInfo(_filepath);
                _headerIndex = new Dictionary<string, int>();

                using (var sr = new StreamReader(_filepath))
                {
                    // Get the headers and the numrows-1 (excludes header)
                    Int32 rowcount = 0;
                    while (!sr.EndOfStream)
                    {
                        if (rowcount == 0)
                        {
                            String[] headers = sr.ReadLine().ToLower().Replace("\"", "").Split(',');

                            // Create dictionary with headers and their column index
                            Int32 headerindex = 0;
                            foreach (var header in headers)
                            {
                                _headerIndex.Add(header, headerindex);
                                headerindex++;
                            }
                        }

                        sr.ReadLine();
                        rowcount++;
                    }

                    // Save the length
                    Length = rowcount;
                }
            }
            else
            {
                throw new FileNotFoundException("Path doesn't exist. path = " + path);
            }
        }


        /// <summary>
        /// Goes through the process of reading the CSV file and creating a 2-dimensional array representing the CSV table.
        /// </summary>
        private void CreateTable()
        {
            using (var sr = new System.IO.StreamReader(_filepath))
            {
                _table = new object[this.Length][];

                // Skip header row
                sr.ReadLine();

                Int32 rowindex = 0;
                while (!sr.EndOfStream)
                {
                    // Read row, remove newline, remove quotations from strings, and add row values to the table variable
                    String cleanRow = sr.ReadLine().Replace(Environment.NewLine, String.Empty);
                    String[] values = cleanRow.Replace("\"", "").Split(',');
                    _table[rowindex] = values;
                    rowindex++;
                }
            }
        }

        /// <summary>
        /// Allows you to retrieve the entire table.
        /// </summary>
        /// <returns>Returns a Object[][] type representing the CSV table.</returns>
        public Object[][] Table
        {
            get
            {
                if (_table == null) CreateTable();

                return _table;
            }
        }

        /// <summary>
        /// Retrieve a specific value by providing the header name and row index.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="rowindex"></param>
        /// <returns>Returns a String type representing the CSV value.</returns>
        public Object this[string key, int rowindex]
        {
            get
            {
                if (rowindex > (this.Length - 1))
                {
                    throw new IndexOutOfRangeException("The row index provided exceeds the length of file. Length == " + this.Length);
                }

                if (_table == null) CreateTable();

                Int32 colindex = _headerIndex[key];
                return _table[rowindex][colindex];
            }
        }

        /// <summary>
        /// Retrieve an entire column by providing the header name as the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Returns a String[] type representing a CSV column.</returns>
        public Object[] this[string key]
        {
            get
            {
                if (_table == null) CreateTable();

                Int32 colindex = _headerIndex[key];
                Object[] temp = new Object[this.Length];
                for (int i = 0; i < this.Length; i++)
                {
                    temp[i] = _table[i][colindex];
                }
                return temp;
            }
        }

        public static void Execute()
        {
            // Archive
            String csv1path = @"C:\BLAINE_Temp\rbsp-b-rbspice_lev-3_ESRLEHT_20140101_v1.1.9-09.cdf_dump.csv";
            CSVFile csv1 = new CSVFile(csv1path);
            object[] epochObjArr1 = csv1["epoch"];

            var propDescriptor = TypeDescriptor.GetProperties(epochObjArr1[0]);
            var typeConverter = TypeDescriptor.GetConverter(epochObjArr1[0]);
            var converted = typeConverter.ConvertFrom(epochObjArr1[0]);
            if (converted.GetType() == typeof(DateTime))
            {
                DateTime dt1 = (DateTime)epochObjArr1[0];
                DateTime dt2 = (DateTime)epochObjArr1[1];

                var dtdiff = dt2 - dt1;
            }
            

            // Production
            String csv2path = @"C:\BLAINE_Temp\rbsp-b-rbspice_lev-3_ESRLEHT_20140101_v2.2.10-00.cdf_dump.csv";
            CSVFile csv2 = new CSVFile(csv2path);
            object epochObj2 = csv1["epoch", csv1.Length - 1];

            Console.WriteLine();
        }

        public static long RowCount(string csvFullPath)
        {
            if (File.Exists(csvFullPath))
            {
                FileInfo fi = new FileInfo(csvFullPath);
                long lineNum = 0;
                using (var sr = new StreamReader(fi.FullName))
                {
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                        lineNum++;
                    }
                }
                return lineNum;
            }
            else
            {
                throw new Exception("CSVFile.RowCount: csv file path does not exist.");
            }
        }

        public static void PurgeEveryNthRecord(string csvFullPath, int n)
        {
            if (File.Exists(csvFullPath))
            {
                FileInfo fi = new FileInfo(csvFullPath);
                FileInfo tmpFi = new FileInfo(System.IO.Path.GetTempFileName());

                using (var sr = new StreamReader(fi.FullName))
                using (var sw = new StreamWriter(tmpFi.FullName))
                {
                    long lineNum = 0;
                    while (!sr.EndOfStream)
                    {
                        string lineContents = sr.ReadLine();
                        if (lineNum % n == 0)
                        {
                            sw.WriteLine(lineContents);
                        }
                        lineNum++;
                    }
                }

                tmpFi.CopyTo(fi.FullName, true);
                tmpFi.Delete();
            }
        }
    }
}
