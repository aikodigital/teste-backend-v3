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

            var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);

            totalAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(perf, play);

            volumeCredits += _creditoEspectador.CalculaCredito(perf, play);

            writer.WriteStartAttribute("item");
            writer.WriteElementString("AmountOwed", Convert.ToString(totalAmount));
            writer.WriteElementString("EarnedCredits", Convert.ToString(volumeCredits));
            writer.WriteElementString("Seats", Convert.ToString(perf.Audience));
            writer.WriteEndAttribute();
        }

        writer.WriteEndElement();

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load("TheatricalPlayers.xml");

        return xmlDocument;
    }
}
