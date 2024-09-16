using System;

namespace TheatricalPlayersRefactoringKata.Services;

public class CalculadoraDePecas
{
    // Intervalo de linhas obrigatório
    private const int minimoDeLinhas = 1000;
    private const int maximoDeLinhas = 4000;

    public int AjustarQuantidadeDeLinhas(int linhas)
    {
        return Math.Clamp(linhas, minimoDeLinhas, maximoDeLinhas);
    }

    // Cálculo do valor da peça teatral,
    // Para cadastro de novos genêros basta alterar o Enum.Genero, acrescentar a nova regra de cálculo e a nova formatação de saída
    public decimal CalculaValorDaPeca(Genero genero, int audiencia, decimal valorBase = 0)
    {
        decimal calculoDaPeca = 0;

        switch (genero)
        {
            case Genero.Tragedy:
                calculoDaPeca = valorBase + ((audiencia > 30) ? 1000 * (audiencia - 30) : 0);
                break;
            case Genero.Comedy:
                calculoDaPeca = valorBase + ((audiencia > 20) ? 300 * audiencia + (10000 + 500 * (audiencia - 20)) : 300 * audiencia);
                break;
            case Genero.Historic:
                calculoDaPeca = (valorBase * 2) + ((audiencia > 30) ? 1000 * (audiencia - 30) : 0) + ((audiencia > 20) ? 300 * audiencia + (10000 + 500 * (audiencia - 20)) : 300 * audiencia);
                break;
            default:
                throw new Exception($"Gênero desconhecido: {genero}");
        }
        calculoDaPeca = calculoDaPeca / 100;
        return calculoDaPeca;
    }
}