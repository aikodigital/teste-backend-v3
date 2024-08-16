using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Calculo_Credito;
using TheatricalPlayersRefactoringKata.Factory;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

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

    private string RecebeDadosParaSerializaçao(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("<Customer>{0}</Customer>\n<Items>\n", invoice.Customer);

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];

            var calculoEmRelacaoAoTipo = _factory.FabricaDeTypes(play);

            totalAmount += calculoEmRelacaoAoTipo.CalculaValoresBase(perf, play);

            volumeCredits += _creditoEspectador.CalculaCredito(perf, play);

            result += String.Format("  <Item>\n    <AmountOwed>{0}</AmountOwed>\n",
                      Convert.ToDecimal(totalAmount));

            result += String.Format("    <EarnedCredits>{0}</EarnedCredits>\n", volumeCredits);

            result += String.Format("    <Seats>{0}<Seats>\n  </Item>\n", perf.Audience);
        }

        result += String.Format("</Items>\n<AmountOwed>{0}</AmountOwed>\n", Convert.ToDecimal(totalAmount));
        result += String.Format("<EarnedCredits>{0}</EarnedCredits>\n", volumeCredits);

        return result;
    }

    public async Task SerializandoDados(Invoice invoice, Dictionary<string, Play> plays)
    {
        try
        {
            var caminhoDestino = @"C:\Users\TheatricalPlayers";
            var arquivoXml = @"TheatricalPlayers.xml";

            var dadosASeremSerializados = RecebeDadosParaSerializaçao(invoice, plays);

            var dir = Directory.CreateDirectory(caminhoDestino);

            var caminhoCompleto = Path.Combine(caminhoDestino, arquivoXml);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(string));

            await using (StreamWriter streamWriter = new StreamWriter(caminhoCompleto))
            {
                xmlSerializer.Serialize(streamWriter, dadosASeremSerializados);
            }
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
}
