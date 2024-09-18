using aikodigital_teste_backend_v3.Models;
using aikodigital_teste_backend_v3.Dto;
using aikodigital_teste_backend_v3.Data;
using System.Globalization;
using System.Text;
using System.Xml;

namespace aikodigital_teste_backend_v3
{
    public class StatementPrinterService
    {
        private static readonly object _txtFileLock = new object();
        private static readonly object _xmlFileLock = new object();
        private readonly ApplicationDbContext _context;
        private const string TXT = "txt"; 
        private const string XML = "xml"; 


        public StatementPrinterService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Print(StatementPrintDto statementPrintDto)
        {
            Invoice invoice = statementPrintDto.invoice;
            Dictionary<string, Play> plays = statementPrintDto.plays;

            switch (statementPrintDto.format)
            {
                case TXT:
                    {
                        string result = await GenerateTxtData(invoice, plays);

                        await SaveStatementInDb(invoice, result);

                        SaveTxtToFile(result);
                    }
                    break;
                case XML:
                    {
                        string result = await GenerateXmlData(invoice, plays);

                        await SaveStatementInDb(invoice, result); ;

                        SaveXmlToFile(result);
                    }
                    break;
                default:
                    throw new Exception("unknown format: " + statementPrintDto.format);
            }
        }

        public async Task<string> GenerateTxtData(Invoice invoice, Dictionary<string, Play> plays)
        {
            string statement = string.Format("Statement for {0}\n", invoice.Customer);
            CultureInfo cultureInfo = new CultureInfo("en-US");
            int volumeCredits = 0;
            int totalAmount = 0;

            foreach (Performance perf in invoice.Performances)
            {
                Play play = plays[perf.PlayId];

                int lines = play.Lines;
                if (lines < 1000) lines = 1000;
                if (lines > 4000) lines = 4000;

                Task<int> thisAmountTask = ThisAmountCalculator(lines, play.Type, perf.Audience);
                Task<int> volumeCreditsTask = VolumeCreditsCalculator(volumeCredits, play.Type, perf.Audience);

                int thisAmount = await thisAmountTask;
                volumeCredits = await volumeCreditsTask;

                // print line for this order
                statement += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100.0m), perf.Audience);
                totalAmount += thisAmount;
            }

            statement += String.Format(cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100.0m));
            statement += String.Format("You earned {0} credits\n", volumeCredits);

            return statement;
        }

        public async Task<string> GenerateXmlData(Invoice invoice, Dictionary<string, Play> plays)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            int totalAmount = 0;
            int volumeCredits = 0;

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                IndentChars = "  ",
                NewLineChars = "\r\n",
            };

            using (MemoryStream memoryStream = new MemoryStream())
            using (XmlWriter writer = XmlWriter.Create(memoryStream, xmlWriterSettings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Statement");

                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");

                writer.WriteElementString("Customer", System.Security.SecurityElement.Escape(invoice.Customer));

                writer.WriteStartElement("Items");
                foreach (Performance perf in invoice.Performances)
                {
                    Play play = plays[perf.PlayId];
                    int lines = play.Lines;
                    if (lines < 1000) lines = 1000;
                    if (lines > 4000) lines = 4000;

                    int thisAmount = await ThisAmountCalculator(lines, play.Type, perf.Audience);
                    int earnedCredits = await VolumeCreditsCalculator(0, play.Type, perf.Audience);

                    writer.WriteStartElement("Item");
                    writer.WriteElementString("AmountOwed", (thisAmount / 100.0m).ToString(cultureInfo));
                    writer.WriteElementString("EarnedCredits", earnedCredits.ToString());
                    writer.WriteElementString("Seats", perf.Audience.ToString());
                    writer.WriteEndElement();

                    totalAmount += thisAmount;
                    volumeCredits += earnedCredits;
                }
                writer.WriteEndElement();

                writer.WriteElementString("AmountOwed", (totalAmount / 100.0m).ToString(cultureInfo));
                writer.WriteElementString("EarnedCredits", volumeCredits.ToString());

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();

                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        private void SaveTxtToFile(string txtContent)
        {
            // Specify base file name and directory
            string fileName = "statement";
            string fileExtension = ".txt";
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(directory, fileName + fileExtension);
            int fileCount = 1;

            // Check if the file already exists
            while (File.Exists(filePath))
            {
                filePath = Path.Combine(directory, $"{fileName}_{fileCount}{fileExtension}");
                fileCount++;
            }

            // Lock to ensure that only one process saves the file at a time
            lock (_txtFileLock)
            {
                // Save TXT content to the specified file
                File.WriteAllText(filePath, txtContent, Encoding.UTF8);
                Console.WriteLine($"TXT file saved to: {filePath}");
            }
        }

        private void SaveXmlToFile(string xmlContent)
        {
            // Specify base file name and directory
            string fileName = "statement";
            string fileExtension = ".xml";
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(directory, fileName + fileExtension);
            int fileCount = 1;

            // Check if the file already exists
            while (File.Exists(filePath))
            {
                filePath = Path.Combine(directory, $"{fileName}_{fileCount}{fileExtension}");
                fileCount++;
            }

            // Lock to ensure that only one process saves the file at a time
            lock (_xmlFileLock)
            {
                // Save XML content to the specified file
                File.WriteAllText(filePath, xmlContent, Encoding.UTF8);
                Console.WriteLine($"XML file saved to: {filePath}");
            }
        }

        private async Task<int> ThisAmountCalculator(int lines, string type, int audience)
        {
            int thisAmount = lines * 10;

            switch (type)
            {
                case "tragedy":
                    if (audience > 30)
                    {
                        thisAmount += 1000 * (audience - 30);
                    }
                    break;
                case "comedy":
                    if (audience > 20)
                    {
                        thisAmount += 10000 + 500 * (audience - 20);
                    }
                    thisAmount += 300 * audience;
                    break;
                case "history":
                    int tragedyAmount = await ThisAmountCalculator(lines, "tragedy", audience);
                    int comedyAmount = await ThisAmountCalculator(lines, "comedy", audience);
                    thisAmount = tragedyAmount + comedyAmount;
                    break;
                default:
                    throw new Exception("unknown type: " + type);
            }

            return await Task.FromResult(thisAmount);
        }

        private async Task<int> VolumeCreditsCalculator(int volumeCredits, string type, int audience)
        {
            // add volume credits
            volumeCredits += Math.Max(audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == type) volumeCredits += (int)Math.Floor((decimal)audience / 5);

            return await Task.FromResult(volumeCredits);
        }

        public async Task SaveStatementInDb(Invoice invoice, string result)
        {
            // Add the data to the database before saving the file
            foreach (Performance perf in invoice.Performances)
            {
                PerformanceStatement performanceStatement = new PerformanceStatement
                {
                    Statement = result,
                    Customer = invoice.Customer,
                    PlayId = perf.PlayId,
                    Audience = perf.Audience
                };

                await _context.PerformanceStatement.AddAsync(performanceStatement);
            }

            await _context.SaveChangesAsync();
        }
    }
}