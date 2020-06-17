using Newtonsoft.Json;
using SPDF.CDF.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Utilities.Converters
{
    public class CDFConverter
    {
        private CDF_File _cdf = null;
        private String _tmpxml = String.Empty;
        public CDF_File CDF { get; set; }
        public XmlElement XML { get; set; }
        public XmlDocument CDFXml { get; set; }

        public CDFConverter(FileInfo fi)
        {
            if (fi.Extension.Contains(".cdf"))
            {
                if (fi.Exists) CDF = new CDF_File(fi.FullName);
                _tmpxml = @"C:\PATH_TO_CDFML_DIR\galileo_epd_jup-I2-recmode-64sector_19960911_orb-g2-psx_ver-1.1.1-152.xml";
                if (_tmpxml.Contains("PATH_TO_CDFML_DIR")) throw new Exception("Must provide CDFML directory in _tmpxml path.");               
                CDFXml = new XmlDocument();
                CDFXml.Load(_tmpxml);
                XML = CDFXml.DocumentElement;
            }
        }

        public void DeleteTmpFiles()
        {
            if (File.Exists(_tmpxml)) File.Delete(_tmpxml);
        }

        private XmlElement ConvertToXML()
        {
            XmlDocument doc = new System.Xml.XmlDocument();
            return CDF.ToXml(doc);
        }

        public String ConvertToJSON()
        {
            return Newtonsoft.Json.JsonConvert.SerializeXmlNode(XML);
        }

        public static void Execute(string cdfpath = "")
        {
            if (cdfpath == "") cdfpath = @"PATH_TO_CDF";
            if (cdfpath.Contains("PATH_TO_CDF")) throw new Exception("Must provide path to a NASA CDF file.");

            if (File.Exists(cdfpath))
            {
                CDFConverter conv = new CDFConverter(new FileInfo(cdfpath));
                String str = conv.ConvertToJSON();

                Console.WriteLine(str);
                conv.DeleteTmpFiles();
            }
        }
    }
}
