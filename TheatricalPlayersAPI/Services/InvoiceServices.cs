using System.Net;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;

namespace TheatricalPlayersAPI.Services;

public class InvoiceServices
{
    private TheatricalDbContext _context;
    private Response<InvoiceResponseModel> _response;
    private Response<List<InvoiceResponseModel>> _responseList;
    
    public InvoiceServices(TheatricalDbContext context){
        _context = context;
        _response = new();
        _responseList = new();
    }
    
    public async Task<Response<InvoiceResponseModel>> Create(InvoiceModel request, List<int> performanceIds){
        
        var query = _context.Performances.Where(model => performanceIds.Contains(model.Id));
        var performances = query.ToList();
        
        var invoice = new InvoiceModel {
            Performances = performances,
            Customer = request.Customer
        };
        
        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();
        
        var performanceResponseList = GenericServices.GeneratePerfResponse(performances);

        var invoiceResponse = new InvoiceResponseModel {
            Customer = request.Customer,
            Performances = performanceResponseList
        };

        _response.data = invoiceResponse;
        _response.message = "Invoice created successfully";
        _response.statusCode = HttpStatusCode.Created; 
        return _response;
    }

    public async Task<Response<InvoiceResponseModel>> Update(int id, InvoiceModel request, List<int> performance_ids){
        
        var invoice = _context.Invoices.FirstOrDefault(model => model.Id == id);

        if (invoice == null){
            _response.message = "Invoice not found";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        var query = _context.Performances.Where(model => performance_ids.Contains(model.Id));
        var performances = query.ToList();

        invoice.Performances = performances;
        invoice.Customer = request.Customer;
        
        _context.Invoices.Update(invoice);
        await _context.SaveChangesAsync();
        
        var performanceResponseList = GenericServices.GeneratePerfResponse(performances);
        
        var invoiceResponse = new InvoiceResponseModel {
            Customer = request.Customer,
            Performances = performanceResponseList
        };
        
        _response.data = invoiceResponse;
        _response.message = "Invoice updated successfully";
        _response.statusCode = HttpStatusCode.OK;

        return _response;
    }

    public async Task<Response<List<InvoiceResponseModel>>> GetAll(){
        var invoices = await _context.Invoices.ToListAsync();
        if (invoices.Count == 0){
            _responseList.message = "No invoices found";
            _responseList.statusCode = HttpStatusCode.NotFound;
            return _responseList;
        }

        var invoiceResponses = new List<InvoiceResponseModel>();
        
        foreach (var model in invoices){
            var performances = _context.Performances
                .Where(perf_model => perf_model.InvoiceModelId == model.Id)
                .ToList();
            var performanceResponseList = GenericServices.GeneratePerfResponse(performances);
            var invoiceResponse = new InvoiceResponseModel {
                Customer = model.Customer,
                Performances = performanceResponseList
            };
            invoiceResponses.Add(invoiceResponse);
        }
        
        _responseList.data = invoiceResponses;
        _responseList.message = "Invoices found successfully";
        _responseList.statusCode = HttpStatusCode.OK;

        return _responseList;
    }


    public async Task<Response<InvoiceResponseModel>> GetById(int id){
        var invoice = await _context.Invoices.FirstOrDefaultAsync(model => model.Id == id);
        if (invoice == null){
            _response.message = "Invoice doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        var performances = _context.Performances
            .Where(perf_model => perf_model.InvoiceModelId == invoice.Id)
            .ToList();
        
        var performanceResponseList = GenericServices.GeneratePerfResponse(performances);

        var invoiceResponse = new InvoiceResponseModel {
            Customer = invoice.Customer,
            Performances = performanceResponseList
        };
        
        _response.data = invoiceResponse;
        _response.message = "Invoice retrieved successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }

    public async Task<Response<InvoiceResponseModel>> Delete(int id){
        var invoice = _context.Invoices.FirstOrDefault(model => model.Id == id);
        if (invoice == null){
            _response.message = "Invoice doesn't exist";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
        
        var performances = _context.Performances
            .Where(model => model.InvoiceModelId == invoice.Id)
            .ToList();
        
        var performanceResponseList = GenericServices.GeneratePerfResponse(performances);

        var invoiceResponse = new InvoiceResponseModel {
            Customer = invoice.Customer,
            Performances = performanceResponseList
        };
        
        _response.data = invoiceResponse;
        _response.message = "Invoice deleted successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }
}