using System.Net;
using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersAPI.Services;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersAPI.Controllers;

[Route("invoice")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceServices _invoiceService;

    public InvoiceController(InvoiceServices invoiceService){
        _invoiceService = invoiceService;
    }

    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<InvoiceModel>>> Create([FromBody] InvoiceModel request, [FromQuery] List<int> performanceIds)
    {
        var invoice = await _invoiceService.Create(request, performanceIds);
        if (invoice.statusCode == HttpStatusCode.NotFound) return NotFound(invoice);
        return Created("", invoice);
    }

    [HttpPut("update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<InvoiceResponseModel>>> Update(int id, [FromBody] InvoiceModel request, [FromQuery] List<int> performanceIds)
    {
        var invoice = await _invoiceService.Update(id, request, performanceIds);
        if(invoice.statusCode == HttpStatusCode.NotFound) return NotFound(invoice);
        return Ok(invoice);
    }

    [HttpGet("getAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<List<InvoiceResponseModel>>>> GetAll()
    {
        var invoices = await _invoiceService.GetAll();
        if(invoices.statusCode == HttpStatusCode.NotFound) return NotFound(invoices);
        return Ok(invoices);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<InvoiceResponseModel>>> GetById(int id)
    {
        var invoice = await _invoiceService.GetById(id);
        if(invoice.statusCode == HttpStatusCode.NotFound) return NotFound(invoice);
        return Ok(invoice);
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Response<InvoiceResponseModel>>> Delete(int id)
    {
        var invoice = await _invoiceService.Delete(id);
        if(invoice.statusCode == HttpStatusCode.NotFound) return NotFound(invoice);
        return Ok(invoice);
    }


}