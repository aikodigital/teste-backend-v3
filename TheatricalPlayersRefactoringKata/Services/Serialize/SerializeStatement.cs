using System;
using System.IO;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
namespace TheatricalPlayersRefactoringKata.Services.Serialize;

public class SerializeStatement{
    
    public static string SerializeToXMl(StatementToXml statement)
    {
        if (statement == null) throw new ArgumentNullException("Statement objetc cannot be Null.");
        string result = string.Empty;
        XmlSerializer serializer = new XmlSerializer(typeof(StatementToXml));

        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Making the patter to UTF8 with BOM
            using (StreamWriter streamWriter = new StreamWriter(memoryStream, System.Text.Encoding.UTF8))
            {
                serializer.Serialize(streamWriter, statement);
                result = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }
        return result;
    }
}