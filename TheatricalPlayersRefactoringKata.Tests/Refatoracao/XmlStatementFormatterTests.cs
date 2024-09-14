using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Formatter;
using TheatricalPlayersRefactoringKata.Interface;
using TheatricalPlayersRefactoringKata.Model;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests.Refatoracao;
public class XmlStatementFormatterTests
{
    [Fact]
    public void ShouldFormatTextStatementCorrectly()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new ("Hamlet", 4024, "tragedy") },
            { "as-like", new ("As You Like It", 2670, "comedy") },
            { "othello", new ("Othello", 3560, "tragedy") }
        };

        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new ("hamlet", 55),
                new("as-like", 35),
                new ("othello", 40),
            }
        );

        IStatementFormatter formatter = new XmlStatementFormatter();
        string result = formatter.FormatAsync(invoice, plays).Result;

        string expected =
            @"<Statement>
  <Customer>BigCo</Customer>
  <Performances>
    <Performance>
      <Play>Hamlet</Play>
      <Audience>55</Audience>
      <Amount>650</Amount>
    </Performance>
    <Performance>
      <Play>As You Like It</Play>
      <Audience>35</Audience>
      <Amount>547</Amount>
    </Performance>
    <Performance>
      <Play>Othello</Play>
      <Audience>40</Audience>
      <Amount>456</Amount>
    </Performance>
  </Performances>
  <TotalAmount>1653</TotalAmount>
  <Credits>47</Credits>
</Statement>";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldHandleEmptyInvoice()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024, "tragedy") }
        };

        var invoice = new Invoice("BigCo", new List<Performance>());

        IStatementFormatter formatter = new XmlStatementFormatter();
        string result = formatter.FormatAsync(invoice, plays).Result;

        string expected = @"<Statement>
  <Customer>BigCo</Customer>
  <Performances />
  <TotalAmount>0</TotalAmount>
  <Credits>0</Credits>
</Statement>";

        Assert.Equal(expected, result);
    }

    [Fact]
    public void ShouldCalculateForHistoricalPlay()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new ("Hamlet", 4024, "tragedy") },
            { "henry-v", new ("Henry V", 3227, "history") }
        };

        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new ("hamlet", 55),
                new ("henry-v", 20),
            }
        );

        IStatementFormatter formatter = new XmlStatementFormatter();
        string result = formatter.FormatAsync(invoice, plays).Result;

        string expected = @"<Statement>
  <Customer>BigCo</Customer>
  <Performances>
    <Performance>
      <Play>Hamlet</Play>
      <Audience>55</Audience>
      <Amount>650</Amount>
    </Performance>
    <Performance>
      <Play>Henry V</Play>
      <Audience>20</Audience>
      <Amount>705.4</Amount>
    </Performance>
  </Performances>
  <TotalAmount>1355.4</TotalAmount>
  <Credits>29</Credits>
</Statement>";

        Assert.Equal(expected, result);
    }
}
