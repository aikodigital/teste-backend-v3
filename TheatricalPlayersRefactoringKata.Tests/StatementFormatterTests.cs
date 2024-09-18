using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Formatters;
using TheatricalPlayersRefactoringKata.Interfaces;
using TheatricalPlayersRefactoringKata.Strategies;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementFormatterTests
    {
        private readonly Dictionary<string, Play> _plays;
        private readonly Dictionary<string, IStatementStrategy> _strategies;

        public StatementFormatterTests()
        {
            // Initialize
            _plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 1000, "tragedy") },
                { "as-you-like-it", new Play("As You Like It", 2000, "comedy") }
            };

            _strategies = new Dictionary<string, IStatementStrategy>
            {
                { "tragedy", new TragedyStrategy() },
                { "comedy", new ComedyStrategy() }
            };
        }

        [Fact]
        public void TestZeroAudience_TextStatementFormatter()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 0)
            });

            var formatter = new TextStatementFormatter();

            // Act
            var result = formatter.Format(invoice, _plays, _strategies);

            // Assert
            var expected = "Statement for BigCo\n  Hamlet: $0.00 (0 seats)\nAmount owed is $0.00\nYou earned 0 credits\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnknownPlayType_TextStatementFormatter()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("unknownPlayId", 5)
            });

            var formatter = new TextStatementFormatter();

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => formatter.Format(invoice, _plays, _strategies));
            Assert.Equal("The given key 'unknownPlayId' was not present in the dictionary.", ex.Message);
        }

        [Fact]
        public void TestExtremeValues_TextStatementFormatter()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 10000),
                new Performance("as-you-like-it", 5000)
            });

            var formatter = new TextStatementFormatter();

            // Act
            var result = formatter.Format(invoice, _plays, _strategies);

            // Assert
            var expected = "Statement for BigCo\n" +
                           "  Hamlet: $99,800.00 (10000 seats)\n" +
                           "  As You Like It: $40,200.00 (5000 seats)\n" +
                           "Amount owed is $140,000.00\n" +
                           "You earned 15940 credits\n";
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestZeroAudience_XMLStatementFormatter()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("hamlet", 0)
            });

            var formatter = new XmlStatementFormatter();

            // Act
            var result = formatter.Format(invoice, _plays, _strategies);

            // Expected
            var expected = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n" +
                           "<Statement xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n" +
                           "  <Customer>BigCo</Customer>\n" +
                           "  <Items>\n" +
                           "    <Item>\n" +
                           "      <AmountOwed>0</AmountOwed>\n" +
                           "      <EarnedCredits>0</EarnedCredits>\n" +
                           "      <Seats>0</Seats>\n" +
                           "    </Item>\n" +
                           "  </Items>\n" +
                           "  <AmountOwed>0</AmountOwed>\n" +
                           "  <EarnedCredits>0</EarnedCredits>\n" +
                           "</Statement>\n";

            // Normalizar XML
            result = NormalizeXml(result);
            expected = NormalizeXml(expected);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestUnknownPlayType_XMLStatementFormatter()
        {
            // Arrange
            var invoice = new Invoice("BigCo", new List<Performance>
            {
                new Performance("unknownPlayId", 5)
            });

            var formatter = new XmlStatementFormatter();

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => formatter.Format(invoice, _plays, _strategies));
            Assert.Equal("The given key 'unknownPlayId' was not present in the dictionary.", ex.Message);
        }

        private string RemoveXmlDeclaration(string xml)
        {
            // Remove declaração do XML se existente
            const string xmlDeclaration = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n";
            return xml.StartsWith(xmlDeclaration) ? xml.Substring(xmlDeclaration.Length).TrimStart() : xml;
        }

        private string NormalizeXml(string xml)
        {
            // Remove declaração do XML se existente XML e normaliza
            xml = RemoveXmlDeclaration(xml);
            return xml.Replace("\r\n", "\n").Trim();
        }
    }
}
