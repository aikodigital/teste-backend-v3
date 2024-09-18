using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using TheatricalPlayersRefactoringKata.Data;

namespace TheatricalPlayersRefactoringKata.Infrastructure
{
    public class XmlStatementFormatter : IStatementFormatter
    {
        public string Format(StatementData statementData)
        {
            var serializer = new XmlSerializer(typeof(StatementData));
            using (var stringWriter = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
                {
                    writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                    serializer.Serialize(writer, statementData);
                }

                // Converte a saída para UTF-8
                string xmlUtf16 = stringWriter.ToString();
                byte[] bytes = Encoding.UTF8.GetBytes(xmlUtf16);
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
}
