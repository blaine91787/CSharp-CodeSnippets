using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Cryptography
{
    public class HashComparer
    {
        private FileHash _file1 = null;
        private FileHash _file2 = null;
        private bool _hashesComputed = false;
        public FileHash File1
        {
            get
            {
                if (_file1 == null) throw new Exception("File1 is null.");
                return _file1;
            }
            set
            {
                _file1 = value;
            }
        }
        public FileHash File2
        {
            get
            {
                if (_file2 == null) throw new Exception("File2 is null.");
                return _file2;
            }
            set
            {
                _file2 = value;
            }
        }
        public HashComparer()
        {
            File1 = null;
            File2 = null;
            _hashesComputed = false;
        }
        public HashComparer(string file1, string file2, string hashAlgorithm = "sha256")
        {
            try
            {
                GetHashes(file1, file2, hashAlgorithm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void GetHashes(string file1, string file2, string hashAlgorithm = "sha256")
        {
            try
            {
                File1 = new FileHash(file1, hashAlgorithm);
                File2 = new FileHash(file2, hashAlgorithm);

                Task taskA = Task.Run(() => File1.ComputeHash());
                Task taskB = Task.Run(() => File2.ComputeHash());

                Task.WaitAll(new Task[] { taskA, taskB });

                _hashesComputed = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to obtain hash information. Ex: " + ex.Message);
            }
        }
        public bool AreEqual()
        {
            if (File1 == null) throw new Exception("File1 is null.");
            if (File2 == null) throw new Exception("File2 is null.");
            return File1.HashString == File2.HashString;
        } 
        public bool AreEqual(string file1, string file2, string hashAlgorithm = "sha256")
        {
            if (_hashesComputed) Reset();

            GetHashes(file1, file2, hashAlgorithm);

            if (File1 == null) throw new Exception("File1 is null.");
            if (File2 == null) throw new Exception("File2 is null.");

            return File1.HashString == File2.HashString;
        }
        private void Reset()
        {
            File1 = null;
            File2 = null;
            _hashesComputed = false;
        }

        public static void Execute()
        {
            var file1 = @"PATH_TO_FILE1";
            var file2 = @"PATH_TO_FILE2";

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

    public class FileHash
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public FileInfo Fileinfo { get; set; }
        public string HashString
        {
            get 
            {
                if (ComputedHash != null && ComputedHash.Count() != 0)
                    return ByteArrayToString(ComputedHash).ToUpper();
                else
                    throw new Exception("Hash has not been computed. Run this.ComputeHash()");
            }
        }
        public byte[] ComputedHash { get; set; }
        public string HashAlgorithm { get; set; }
        public FileHash(string filepath, string hashalgo)
        {
            if (!File.Exists(filepath)) throw new Exception("File '" + filepath + "' does not exist.");

            Fileinfo = new FileInfo(filepath);
            HashAlgorithm = hashalgo;
            Path = filepath;
            char[] seps = new char[] { '\\', '/' };
            string[] chunks = filepath.Split(seps);
            if (chunks.Length > 0)
            {
                FileName = chunks.Last();
            }
        }
        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        } 
        public void ComputeHash(string hashAlgo = "sha256")
        {
            if (HashAlgorithm.ToLower().Equals("sha256")) ComputeSHA256();
            else if (HashAlgorithm.ToLower().Equals("md5")) ComputeMD5();
            else if (hashAlgo.ToLower().Equals("sha256")) ComputeSHA256();
            else if (hashAlgo.ToLower().Equals("md5")) ComputeMD5();
            else throw new Exception("Hash algorithm not supported by FileHash object.");
        }
        public void ComputeSHA256()
        {
            using (SHA256 sha256 = SHA256Managed.Create())
            {
                using (FileStream fs = File.OpenRead(Path))
                {
                    ComputedHash = sha256.ComputeHash(fs);
                }
            }
        }
        public void ComputeMD5()
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream fs = File.OpenRead(Path))
                {
                    ComputedHash = md5.ComputeHash(fs);
                }
            }
        }
    }
}
