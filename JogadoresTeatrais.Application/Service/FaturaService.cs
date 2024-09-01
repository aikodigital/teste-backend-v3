using JogadoresTeatrais.Application.Interfaces;
using JogadoresTeatrais.Domain.Entities;
using JogadoresTeatrais.Domain.Interfaces;
using JogadoresTeatrais.Utility.Utility;
using JogaresTeatrais.Data;
using System.Text;
using System.Text.Json;

namespace JogadoresTeatrais.Application.Service
{
    public class FaturaService : IFaturaService
    {

        private readonly IFaturaRepository faturaRepository;

        private readonly IJogarRepository jogarRepository;

        public FaturaService(IFaturaRepository faturaRepository, IJogarRepository jogarRepository)
        {
            this.faturaRepository = faturaRepository;
            this.jogarRepository = jogarRepository;
        }


        public string GetAll(string formato = "Json")
        {

            var faturas = faturaRepository.GetAll();
            var jogar = jogarRepository.GetAll();

            Dictionary<int, Jogar> jogarDictionary = new Dictionary<int, Jogar>();

            foreach (var _jogar in jogar)
            {
                jogarDictionary[_jogar.Id] = _jogar;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var fatura in faturas)
            {

                switch (formato.ToLower())
                {
                    case "json":
                        stringBuilder.AppendLine(FormatarFatura(fatura, jogarDictionary, FormatoArquivo.Json));
                        break;
                    case "txt":
                        stringBuilder.AppendLine(FormatarFatura(fatura, jogarDictionary, FormatoArquivo.Txt));
                        break;
                    case "xml":
                        stringBuilder.AppendLine(FormatarFatura(fatura, jogarDictionary, FormatoArquivo.Xml));
                        break;
                    default:
                        throw new ArgumentException("Formato não suportado");
                }
            }
            return stringBuilder.ToString();

        }

        private static string FormatarFatura(Fatura fatura, Dictionary<int, Jogar> jogar, FormatoArquivo formatoArquivo)
        {
            return formatoArquivo switch
            {
                FormatoArquivo.Txt => FormatarTexto(fatura, jogar),
                FormatoArquivo.Xml => GerarXml(fatura, jogar),
                FormatoArquivo.Json => GerarJson(fatura, jogar),
                FormatoArquivo.Csv => throw new NotImplementedException("Formato CSV ainda não implementado!"),
                _ => throw new ArgumentException("Formato de arquivo desconhecido.", nameof(formatoArquivo)),
            };
        }

        private static string FormatarTexto(Fatura fatura, Dictionary<int, Jogar> jogar)
        {
            var resultado = $"Extrato para {fatura.Cliente}\n";
            var valorTotal = CalcularMontanteTotal(fatura, jogar, out int volumeCreditos);

            foreach (var desempenho in fatura.Desempenhos)
            {
                var jogarItem = jogar[desempenho.JogarId];
                var valorDesempenho = CalcularValorDesempenho(desempenho, jogarItem);

                resultado += $"  {jogarItem.Nome}: {valorDesempenho / 100m:C} ({desempenho.Audiencia} assentos)\n";
            }

            resultado += $"Valor devido é {valorTotal / 100m:C}\n";
            resultado += $"Você ganhou {volumeCreditos} créditos\n";

            return resultado;
        }

        private static string GerarXml(Fatura fatura, Dictionary<int, Jogar> jogar)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<Extrato>");
            sb.AppendLine($"  <Cliente>{fatura.Cliente}</Cliente>");
            sb.AppendLine("  <Itens>");

            foreach (var desempenho in fatura.Desempenhos)
            {
                var jogarItem = jogar[desempenho.JogarId];
                var valorDesempenho = CalcularValorDesempenho(desempenho, jogarItem);
                sb.AppendLine("    <Item>");
                sb.AppendLine($"      <ValorDevido>{valorDesempenho / 100m:C}</ValorDevido>");
                sb.AppendLine($"      <CreditosGanhos>{Math.Max(desempenho.Audiencia - 30, 0)}</CreditosGanhos>");
                sb.AppendLine($"      <Assentos>{desempenho.Audiencia}</Assentos>");
                sb.AppendLine("    </Item>");
            }

            sb.AppendLine("  </Itens>");
            sb.AppendLine($"  <ValorDevido>{CalcularMontanteTotal(fatura, jogar, out int volumeCreditos) / 100m:C}</ValorDevido>");
            sb.AppendLine($"  <CreditosGanhos>{volumeCreditos}</CreditosGanhos>");
            sb.AppendLine("</Extrato>");

            return sb.ToString();
        }

        private static string GerarJson(Fatura fatura, Dictionary<int, Jogar> jogar)
        {
            
            var extrato = new
            {
                Cliente = fatura.Cliente,
                Itens = new List<object>(),
                ValorDevido = CalcularMontanteTotal(fatura, jogar, out int volumeCreditos) / 100m,
                CreditosGanhos = volumeCreditos
            };

            foreach (var desempenho in fatura.Desempenhos)
            {
                var jogarItem = jogar[desempenho.JogarId];
                var valorDesempenho = CalcularValorDesempenho(desempenho, jogarItem);

                extrato.Itens.Add(new
                {
                    Jogar = jogarItem.Nome,
                    ValorDevido = valorDesempenho / 100m,
                    CreditosGanhos = Math.Max(desempenho.Audiencia - 30, 0),
                    Assentos = desempenho.Audiencia
                });
            }

            return JsonSerializer.Serialize(extrato, new JsonSerializerOptions { WriteIndented = true });
        }

        private static int CalcularMontanteTotal(Fatura fatura, Dictionary<int, Jogar> jogar, out int creditos)
        {
            var valorTotal = 0;
            creditos = 0;

            foreach (var desempenho in fatura.Desempenhos)
            {
                var jogarItem = jogar[desempenho.JogarId];
                var valorDesempenho = CalcularValorDesempenho(desempenho, jogarItem);

                valorTotal += valorDesempenho;
                creditos += Math.Max(desempenho.Audiencia - 30, 0);

                if (jogarItem.Tipo == "comedy")
                {
                    creditos += (int)Math.Floor((decimal)desempenho.Audiencia / 5);
                }
            }

            return valorTotal;
        }

        private static int CalcularValorDesempenho(Desempenho desempenho, Jogar jogar)
        {
            int valor;
            var linhas = Math.Clamp(jogar.Linhas, 1000, 4000);

            valor = jogar.Tipo switch
            {
                "tragedy" => CalcularValorTragedia(desempenho.Audiencia, linhas),
                "comedy" => CalcularValorComedia(desempenho.Audiencia, linhas),
                "history" => CalcularValorTragedia(desempenho.Audiencia, linhas) + CalcularValorComedia(desempenho.Audiencia, linhas),
                _ => throw new ArgumentException($"Tipo desconhecido: {jogar.Tipo}", nameof(jogar.Tipo)),
            };

            return valor;
        }

        private static int CalcularValorTragedia(int audiencia, int linhas)
        {
            var valor = linhas * 10;
            if (audiencia > 30)
            {
                valor += 1000 * (audiencia - 30);
            }
            return valor;
        }

        private static int CalcularValorComedia(int audiencia, int linhas)
        {
            var valor = linhas * 10;
            if (audiencia > 20)
            {
                valor += 10000 + 500 * (audiencia - 20);
            }
            valor += 300 * audiencia;
            return valor;
        }
    }
}
