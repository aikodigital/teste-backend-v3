using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Domain.Dtos;
using TheatricalPlayersRefactoringKata.Domain.Interfaces.Services;

namespace TheatricalPlayersRefactoringKata.API.Controllers;

[ApiController]
[Route("[controller]/api")]
public class InvoiceController : Controller
{
    private readonly IInvoiceService _invoiceService;
    public InvoiceController(IInvoiceService invoiceService) => _invoiceService = invoiceService;
    
    [HttpGet("get-all")]
    public async Task<IActionResult> Get() => Ok(await _invoiceService.GetAll());

    [HttpGet("get-all-plays")]
    public async Task<IActionResult> GetPlays() => Ok(await _invoiceService.GetAllPlays());

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InvoiceDto invoiceDto)
    {
        try
        {
            if (await _invoiceService.Add(invoiceDto)) return Ok("success");
            else return BadRequest("error db");
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }
}