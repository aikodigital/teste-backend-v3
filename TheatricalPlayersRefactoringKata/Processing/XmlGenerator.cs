using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Processing
{
    public class XmlGenerator
    {
        private readonly string _outputDirectory;

        public XmlGenerator(string outputDirectory)
        {
            _outputDirectory = Path.GetFullPath(outputDirectory);
        }

        public void GenerateXml(string fileName, List<Play> plays)
        {
            if (!Directory.Exists(_outputDirectory))
            {
                Directory.CreateDirectory(_outputDirectory);
            }

            var filePath = Path.Combine(_outputDirectory, fileName);

            var doc = new XDocument(
                new XElement("Plays",
                    from play in plays
                    select new XElement("Play",
                        new XElement("Id", play.Id),
                        new XElement("Title", play.Title),
                        new XElement("Category", play.Category)
                    )
                )
            );

            doc.Save(filePath);
        }
    }
}
