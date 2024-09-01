using Moq;
using Xunit;
using JogadoresTeatrais.Application.Service;
using JogadoresTeatrais.Domain.Interfaces;
using JogadoresTeatrais.Domain.Entities;
using System.Collections.Generic;
using System.Text.Json;
using JogaresTeatrais.Data;

namespace JogadoresTeatrais
{

    public class FaturaServiceTests
    {
        private readonly Mock<IFaturaRepository> _faturaRepositoryMock;
        private readonly Mock<IJogarRepository> _jogarRepositoryMock;
        private readonly FaturaService _faturaService;

        public FaturaServiceTests()
        {
            _faturaRepositoryMock = new Mock<IFaturaRepository>();
            _jogarRepositoryMock = new Mock<IJogarRepository>();
            _faturaService = new FaturaService(_faturaRepositoryMock.Object, _jogarRepositoryMock.Object);
        }

        [Fact]
        public void GetAll_Json()
        {

            SetupMockDados();

            var result = _faturaService.GetAll();
            Assert.NotNull(result);


            var jsonDocument = JsonDocument.Parse(result);


            Assert.Equal("Big Co", jsonDocument.RootElement.GetProperty("Cliente").GetString());
            Assert.Equal(1653, jsonDocument.RootElement.GetProperty("ValorDevido").GetDecimal());
            Assert.Equal(47, jsonDocument.RootElement.GetProperty("CreditosGanhos").GetInt32());

            var firstItem = jsonDocument.RootElement.GetProperty("Itens")[0];
            Assert.Equal("Hamlet", firstItem.GetProperty("Jogar").GetString());
            Assert.Equal(650, firstItem.GetProperty("ValorDevido").GetDecimal());
            Assert.Equal(55, firstItem.GetProperty("Assentos").GetInt32());
            Assert.Equal(25, firstItem.GetProperty("CreditosGanhos").GetInt32());
        }

        [Fact]
        public void GetAll_Txt()
        {

            SetupMockDados();

            var result = _faturaService.GetAll("Txt");
            Assert.NotNull(result);

            Assert.Contains("Extrato para Big Co", result);

            Assert.Contains("Hamlet: R$ 650,00 (55 assentos)", result);
            Assert.Contains("As You Like It: R$ 547,00 (35 assentos)", result);
            Assert.Contains("Othello: R$ 456,00 (40 assentos)", result);

            Assert.Contains("Valor devido é R$ 1.653,00", result);
            Assert.Contains("Você ganhou 47 créditos", result);
        }



        [Fact]
        public void GetAll_Xml()
        {

            SetupMockDados();


            var result = _faturaService.GetAll("xml");


            Assert.NotNull(result);
            Assert.Contains("<?xml version=\"1.0\" encoding=\"utf-8\"?>", result);
            Assert.Contains("<Extrato>", result);
            Assert.Contains("<Cliente>Big Co</Cliente>", result);
            Assert.Contains("<Itens>", result);


            Assert.Contains("<Item>", result);
            Assert.Contains("<ValorDevido>R$ 650,00</ValorDevido>", result);
            Assert.Contains("<CreditosGanhos>25</CreditosGanhos>", result);
            Assert.Contains("<Assentos>55</Assentos>", result);


            Assert.Contains("<Item>", result);
            Assert.Contains("<ValorDevido>R$ 547,00</ValorDevido>", result);
            Assert.Contains("<CreditosGanhos>5</CreditosGanhos>", result);
            Assert.Contains("<Assentos>35</Assentos>", result);

            Assert.Contains("<Item>", result);
            Assert.Contains("<ValorDevido>R$ 456,00</ValorDevido>", result);
            Assert.Contains("<CreditosGanhos>10</CreditosGanhos>", result);
            Assert.Contains("<Assentos>40</Assentos>", result);

            Assert.Contains("</Itens>", result);
            Assert.Contains("<ValorDevido>R$ 1.653,00</ValorDevido>", result);
            Assert.Contains("<CreditosGanhos>47</CreditosGanhos>", result);
            Assert.Contains("</Extrato>", result);
        }
        private void SetupMockDados()
        {
            var faturas = new List<Fatura>
        {
            new Fatura
            {
                Id = 1,
                Cliente = "Big Co",
                Desempenhos = new List<Desempenho>
                {
                    new Desempenho { Id = 1, JogarId = 1, Audiencia = 55 },
                    new Desempenho { Id = 2, JogarId = 2, Audiencia = 35 },
                    new Desempenho { Id = 3, JogarId = 3, Audiencia = 40 }
                }
            }
        };

            var jogares = new List<Jogar>
        {
            new Jogar { Id = 1, Nome = "Hamlet", Linhas = 4024, Tipo = "tragedy" },
            new Jogar { Id = 2, Nome = "As You Like It", Linhas = 2670, Tipo = "comedy" },
            new Jogar { Id = 3, Nome = "Othello", Linhas = 3560, Tipo = "tragedy" }
        };

            _faturaRepositoryMock.Setup(repo => repo.GetAll()).Returns(faturas);
            _jogarRepositoryMock.Setup(repo => repo.GetAll()).Returns(jogares);
        }
    }
}
