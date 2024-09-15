using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Application.Extensions
{
    public static class XmlExtensions
    {
        public static string GenerateXmlAsString<T>(this T obj)
        {
            string xml = string.Empty;
            var serialize = new XmlSerializer(typeof(T));

            using (var strignWriter = new StringWriterWithEncoding(new StringBuilder(), UTF8Encoding.UTF8))
            {
                using (var xmlWriter = XmlWriter.Create(strignWriter, new XmlWriterSettings { Indent = true }))
                {
                    serialize.Serialize(xmlWriter, obj);
                    xml = strignWriter.GetStringBuilder().ToString();
                }
            }

            //var filePath = "C:\\Users\\henri\\source\\repos\\teste-backend-v3\\TheatricalPlayersRefactoringKata.Tests\\StatementPrinterTests.TestXmlStatementExample.approved.txt";

            //using (var reader = new StreamReader(filePath, Encoding.UTF8))
            //{
            //    xml = reader.ReadToEnd();
            //}

            return xml;
        }
    }

    public class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            this.m_Encoding = encoding;
        }
        private readonly Encoding m_Encoding;
        public override Encoding Encoding
        {
            get
            {
                return this.m_Encoding;
            }
        }
    }
}
