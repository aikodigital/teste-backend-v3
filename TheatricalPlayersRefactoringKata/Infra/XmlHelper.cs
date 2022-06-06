using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TheatricalPlayersRefactoringKata.Infra
{
    public static class XmlHelper
    {
        public static string GetXmlDeserializedObject<T>(T obj)
        {
            using var memoryStream = new MemoryStream();
            using var xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Indent = true });

            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(xmlWriter, obj);

            xmlWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            using var sr = new StreamReader(memoryStream, Encoding.Unicode);

            return sr.ReadToEnd();
        }
    }
}
