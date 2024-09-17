using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Services;
using TheatricalWebApi.Models;
using TheatricalWebApi.Services;

[ApiController]
[Route("api/statements")]
public class StatementsController : ControllerBase
{
    private readonly StatementPrinter _xmlPrinter;
    private readonly StatementPrinter _textPrinter;

    public StatementsController(
        XmlStatementFormatter xmlFormatter,
        TextStatementFormatter textFormatter)
    {
        _xmlPrinter = new StatementPrinter(xmlFormatter);
        _textPrinter = new StatementPrinter(textFormatter);
    }

    /// <summary>
    /// Gera um extrato em formato XML.
    /// Exemplo de corpo da requisição:
    /// {
    ///   "Customer": "John Doe",
    ///   "Performances": [
    ///     {
    ///       "Play": {
    ///         "Type": "Comedy",
    ///         "Name": "A Funny Thing Happened",
    ///         "Lines": 100
    ///       },
    ///       "Audience": 150
    ///     },
    ///     {
    ///       "Play": {
    ///         "Type": "Tragedy",
    ///         "Name": "The Tragic Hero",
    ///         "Lines": 200
    ///       },
    ///       "Audience": 75
    ///     },
    ///     {
    ///       "Play": {
    ///         "Type": "Historical",
    ///         "Name": "The Ancient Saga",
    ///         "Lines": 300
    ///       },
    ///       "Audience": 120
    ///     }
    ///   ]
    /// }
    /// </summary>
    /// <param name="invoiceDTO">Dados referentes a fatura (InvoiceDTO).</param>
    /// <returns>Extrato gerado em formato XML.</returns>
    /// 
    [HttpPost("generate/xml")]

    public async Task<IActionResult> GenerateXmlStatement([FromBody] InvoiceDTO invoiceDTO)
    {
        try
        {
            if (invoiceDTO == null)
                return BadRequest("Invalid invoice data.");

         
            var invoice = InvoiceConverter.ConvertToInvoice(invoiceDTO);

     
            var statementXml = await _xmlPrinter.PrintAsync(invoice);

            return Content(statementXml, "application/xml");
        }
        catch (Exception ex)
        {
  
            return StatusCode(500, $"Erro ao gerar extrato em XML: {ex.Message}");
        }
    }

    /// <summary>
    /// Gera um extrato em formato de texto.
    /// Exemplo de corpo da requisição:
    /// {
    ///   "Customer": "John Doe",
    ///   "Performances": [
    ///     {
    ///       "Play": {
    ///         "Type": "Comedy",
    ///         "Name": "A Funny Thing Happened",
    ///         "Lines": 100
    ///       },
    ///       "Audience": 150
    ///     },
    ///     {
    ///       "Play": {
    ///         "Type": "Tragedy",
    ///         "Name": "The Tragic Hero",
    ///         "Lines": 200
    ///       },
    ///       "Audience": 75
    ///     },
    ///     {
    ///       "Play": {
    ///         "Type": "Historical",
    ///         "Name": "The Ancient Saga",
    ///         "Lines": 300
    ///       },
    ///       "Audience": 120
    ///     }
    ///   ]
    /// }
    /// 
    /// </summary>
    /// <param name="invoiceDTO">Dados referentes a fatura (InvoiceDTO).</param>
    /// <returns>Extrato gerado em formato de texto.</returns>
    [HttpPost("generate/txt")]
    public async Task<IActionResult> GenerateTextStatement([FromBody] InvoiceDTO invoiceDTO)
    {
        try
        {
            if (invoiceDTO == null)
                return BadRequest("Invalid invoice data.");

 
            var invoice = InvoiceConverter.ConvertToInvoice(invoiceDTO);

       
            var statementTxt = await _textPrinter.PrintAsync(invoice);

            return Content(statementTxt, "text/plain");
        }
        catch (Exception ex)
        {

            return StatusCode(500, $"Erro ao gerar extrato em texto: {ex.Message}");
        }
    }
}
