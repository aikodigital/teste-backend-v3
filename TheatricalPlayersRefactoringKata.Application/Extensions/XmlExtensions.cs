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
        /// <summary>
        /// Serializes an object of type <typeparamref name="T"/> into an XML string representation.
        /// </summary>
        /// <typeparam name="T">The type of the object to be serialized. It must be a serializable type.</typeparam>
        /// <param name="obj">The object to be serialized into an XML string.</param>
        /// <returns>A string containing the XML representation of the object.</returns>
        /// <remarks>
        /// This method uses <see cref="XmlSerializer"/> to serialize the object. It creates an XML string with UTF-8 encoding and indents the XML for readability.
        /// </remarks>
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
