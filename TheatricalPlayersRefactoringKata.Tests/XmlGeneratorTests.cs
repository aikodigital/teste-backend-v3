using System.Collections.Generic;
using System.IO;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Processing;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class XmlGeneratorTests
    {
        private static readonly string OutputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "XmlArchyves");

        public XmlGeneratorTests()
        {
            if (Directory.Exists(OutputDirectory))
            {
                Directory.Delete(OutputDirectory, true);
            }
        }

        [Fact]
        public void GenerateXml_CreatesXmlFile()
        {
            var plays = new List<Play>
            {
                new Play { Id = 1, Title = "Play 1", Category = "Comedy" },
                new Play { Id = 2, Title = "Play 2", Category = "Tragedy" }
            };

            var xmlGenerator = new XmlGenerator(OutputDirectory);
            var fileName = "test.xml";

            xmlGenerator.GenerateXml(fileName, plays);

            var filePath = Path.Combine(OutputDirectory, fileName);
            Assert.True(File.Exists(filePath), "The XML file was not created.");

            var xmlContent = File.ReadAllText(filePath);
            Assert.Contains("<Play>", xmlContent);
        }
    }
}
