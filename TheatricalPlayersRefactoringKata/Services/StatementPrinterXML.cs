using System.IdentityModel.Tokens.Jwt;
using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using TheatricalPlayersRefactoringKata.Domain.DTO;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Models;

namespace TheatricalPlayersRefactoringKata.Domain.Services
{
    public class StatementPrinterXML : IStatementFormatter
    {
        public string Print(StatementDTO statement)
        {
            var xmlSerializer = new XmlSerializer(typeof(StatementDTO));

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(true), // Sem BOM (Byte Order Mark)
                Indent = true, // Para formatação legível
                OmitXmlDeclaration = false // Inclui a declaração XML
            };

            using (var stream = new MemoryStream())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                xmlSerializer.Serialize(writer, statement);

                // Converter o MemoryStream para string UTF-8
                string result = Encoding.UTF8.GetString(stream.ToArray());
                return result;
            }
        }
    }
}
