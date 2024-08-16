using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.Factory;
using System.Threading.Tasks;
using System.Xml;

namespace TheatricalPlayersRefactoringKata.XML;

public class SerializaçaoDadosXml : ISerializaçaoDados
{
    private readonly IPlayTypeFactory _factory;
    private readonly ICalculoCreditoEspectador _creditoEspectador;

    public SerializaçaoDadosXml(IPlayTypeFactory factory, ICalculoCreditoEspectador creditoEspectador)
    {
        _factory = factory;
        _creditoEspectador = creditoEspectador;
    }

    public XmlDocument SerializandoDados(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;

        XmlWriter writer = XmlWriter.Create("TheatricalPlayers.xml");

        writer.WriteStartDocument();
        writer.WriteElementString("Customer", invoice.Customer);
        writer.WriteStartElement("items");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var thisAmount = 0;
            var credits = 0;

            var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);

            thisAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(perf, play);

            credits += _creditoEspectador.CalculaCredito(perf, play);

            writer.WriteStartAttribute("item");
            writer.WriteElementString("AmountOwed", Convert.ToString(thisAmount));
            writer.WriteElementString("EarnedCredits", Convert.ToString(credits));
            writer.WriteElementString("Seats", Convert.ToString(perf.Audience));
            writer.WriteEndAttribute();

            totalAmount += thisAmount;
            volumeCredits += credits;
        }

        writer.WriteEndElement();
        writer.WriteElementString("AmountOwed", Convert.ToString(totalAmount));
        writer.WriteElementString("EarnedCredits", Convert.ToString(volumeCredits));
        writer.WriteEndDocument();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load("TheatricalPlayers.xml");

        return xmlDocument;
    }
}
