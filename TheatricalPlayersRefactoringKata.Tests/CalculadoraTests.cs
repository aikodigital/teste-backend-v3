using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class CalculadoraTests
{    
    [Theory]
    [InlineData(0, 1000)]
    [InlineData(500, 1000)]
    [InlineData(2000, 2000)]
    [InlineData(5000, 4000)]
    [InlineData(10000, 4000)]
    public void TestAjustarQuantidadeDeLinhas(int linhas, int resultadoEsperado)
    {
        CalculadoraDePecas calculadora = new CalculadoraDePecas();
        var resultado = calculadora.AjustarQuantidadeDeLinhas(linhas);
        Assert.Equal(resultadoEsperado, resultado);
    }

    [Fact]
    public void TestCalculaValorDaPecaTragedia()
    {
        CalculadoraDePecas calculadora = new CalculadoraDePecas();
        var result = calculadora.CalculaValorDaPeca(Genero.Tragedy, 30, 3000);
        Assert.Equal(30, result);
    }

    [Fact]
    public void TestCalculaValorDaPecaComedia()
    {
        CalculadoraDePecas calculadora = new CalculadoraDePecas();
        decimal result = calculadora.CalculaValorDaPeca(Genero.Comedy, 30, 3000);
        Assert.Equal(270, result);
    }

    [Fact]
    public void TestCalculaValorDaPecaHistorica()
    {
        CalculadoraDePecas calculadora = new CalculadoraDePecas();
        decimal result = calculadora.CalculaValorDaPeca(Genero.Historic, 30, 3000);
        Assert.Equal(300, result);
    }

    // Uma peça histórica deve equivaler a soma de uma trágedia e uma comédia
    [Fact]
    public void TestCalculaValorDaSomaDaPecaHistorica()
    {
        CalculadoraDePecas calculadora = new CalculadoraDePecas();
        decimal valorComedia = calculadora.CalculaValorDaPeca(Genero.Comedy, 200, 7000);
        decimal valorTragedia = calculadora.CalculaValorDaPeca(Genero.Tragedy, 200, 7000);
        decimal valorHistorica = calculadora.CalculaValorDaPeca(Genero.Historic, 200, 7000);
        Assert.Equal(valorHistorica, valorComedia + valorTragedia);
    }
}