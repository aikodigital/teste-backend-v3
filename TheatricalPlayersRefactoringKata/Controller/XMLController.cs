using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.Factory;

namespace TheatricalPlayersRefactoringKata.XML
{
    public class SerializaçaoDadosXml : IXML
    {
        private readonly IFazerTipos _factory;
        private readonly ICreditosEspectador _creditoEspectador;

        public SerializaçaoDadosXml(IFazerTipos factory, ICreditosEspectador creditoEspectador)
        {
            _factory = factory;
            _creditoEspectador = creditoEspectador;
        }

        public XmlDocument SerializandoDados(InvoiceModel invoice, Dictionary<string, PlayModel> plays)
        {
            decimal totalAmount = 0;
            decimal volumeCredits = 0;

            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = " ",
                Encoding = Encoding.UTF8,
            };

            using (XmlWriter writer = XmlWriter.Create("TheatricalPlayers.xml", settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("Statement");
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");

                writer.WriteElementString("Customer", invoice.Customer);
                writer.WriteStartElement("Items");

                foreach (var perf in invoice.Performances)
                {
                    var play = plays[perf.PlayId];
                    decimal thisAmount = 0;
                    decimal credits = 0;

                    var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);
                    thisAmount += calculoEmRelacaoAoTipo.CalculoBase(perf, play);
                    credits += _creditoEspectador.CalculoCredito(perf, play);

                    writer.WriteStartElement("Item");

                    if (thisAmount % 1 == 0)
                    {
                        writer.WriteElementString("AmountOwed", thisAmount.ToString("F0", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        writer.WriteElementString("AmountOwed", thisAmount.ToString("F1", CultureInfo.InvariantCulture));
                    }

                    writer.WriteElementString("EarnedCredits", credits.ToString());
                    writer.WriteElementString("Seats", perf.Audience.ToString());
                    writer.WriteEndElement();

                    totalAmount += thisAmount;
                    volumeCredits += credits;
                }

                writer.WriteEndElement();
                if (totalAmount % 1 == 0)
                {
                    writer.WriteElementString("AmountOwed", totalAmount.ToString("F1", CultureInfo.InvariantCulture));
                }
                else
                {
                    writer.WriteElementString("AmountOwed", totalAmount.ToString("F1", CultureInfo.InvariantCulture));
                }
                writer.WriteElementString("EarnedCredits", volumeCredits.ToString());
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("TheatricalPlayers.xml");

            return xmlDocument;
        }

    }
}

