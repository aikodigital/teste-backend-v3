using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheatricalPlayersAPI.Context;
using TheatricalPlayersAPI.Models;
using TheatricalPlayersRefactoringKata;

namespace TheatricalPlayersAPI.Services;

public class StatementServices
{
    private TheatricalDbContext _context;
    private Response<StatementModel> _response;
    private Response<List<StatementModel>> _responseList;
    
    public StatementServices(TheatricalDbContext context){
        _context = context;
        _response = new();
        _responseList = new();
    }

    public async Task<Response<StatementModel>> Create([FromBody] StatementModel request){
        _context.Statements.Add(request);
        await _context.SaveChangesAsync();
        
        _response.data = request;
        _response.message = "Statement created successfully";
        _response.statusCode = HttpStatusCode.OK;
        return _response;
    }

    public async Task<Response<StatementModel>> Update([FromQuery] int id, [FromBody] StatementModel request){
        var statement = _context.Statements.FirstOrDefault(state => state.Id == id);
        if (statement == null){
            _response.message = "Statement not found";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        _context.Statements.Update(statement);
        await _context.SaveChangesAsync();
        
        _response.data = request;
        _response.message = "Statement updated successfully";
        _response.statusCode = HttpStatusCode.OK;
        return _response;
    }


    public async Task<Response<StatementModel>> GetById(int id){
        throw new NotImplementedException();
    }

    public async Task<Response<List<StatementModel>>> GetAll(){
        var statements = await _context.Statements.ToListAsync();
        if (statements == null){
            _response.message = "No statements found";
            _response.statusCode = HttpStatusCode.NotFound;
            return _responseList;
        }
        
        _responseList.data = statements;
        _responseList.message = "All statements retrieved successfully";
        _responseList.statusCode = HttpStatusCode.OK;
        
        return _responseList;
    }

    public async Task<Response<StatementModel>> Delete(int id){
        var statement = _context.Statements.FirstOrDefault(state => state.Id == id);

        if (statement == null){
            _response.message = "Statement not found";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        _response.data = statement;
        _response.message = "Statement deleted successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }
    
    public async Task<Response<StatementModel>> Generate(int invoiceId, string printMode){
        var is_print_mode_valid = GenericServices.validatePrintModes(printMode.ToLower());
        if (!is_print_mode_valid){
            _response.message = $"Invalid print mode, should be {StatementPrinter.TEXT_MODE}, " +
                                $"{StatementPrinter.XML_MODE} or " +
                                $"{StatementPrinter.ALL_MODES}";
            _response.statusCode = HttpStatusCode.BadRequest;
            return _response;
        }
        
        var invoiceModel = await _context.Invoices.FirstOrDefaultAsync(invoice => invoice.Id == invoiceId);
        if (invoiceModel == null){
            _response.message = "Invoice not found";
            _response.statusCode = HttpStatusCode.NotFound;
            return _response;
        }
        
        var performancesList = await _context.Performances
            .Where(model => model.InvoiceModelId == invoiceId)
            .ToListAsync();
        
        var statementPrinter = new StatementPrinter();
        var performances = GenericServices.GeneratePerfs(performancesList);
        var invoice = new Invoice(invoiceModel.Customer, performances);
        var plays = new Dictionary<string, Play>();
        
        foreach (var model in performances){
            var playModel = await _context.Plays.FirstOrDefaultAsync(p => p.Name == model.PlayId);
            if (playModel == null){
                continue;
            }
            var play = new Play(playModel.Name, playModel.Lines, playModel.Genre);
            plays.Add(model.PlayId, play);
        }

        string statement = "";
        StatementModel statementModel = new StatementModel();
        if (printMode.ToLower() == StatementPrinter.TEXT_MODE){
            statementModel.Statement =  statementPrinter.Print(invoice, plays, StatementPrinter.TEXT_MODE);;
            statementModel.StatementXml = "";
        }
        else if(printMode.ToLower() == StatementPrinter.XML_MODE){
            statementModel.Statement = "";
            statementModel.StatementXml = statementPrinter.Print(invoice, plays, StatementPrinter.XML_MODE);;
        }else if (printMode.ToLower() == StatementPrinter.ALL_MODES){
            statementModel.Statement = statementPrinter.Print(invoice, plays, StatementPrinter.TEXT_MODE);
            statementModel.StatementXml = statementPrinter.Print(invoice, plays, StatementPrinter.XML_MODE); 
        }
        
        _context.Statements.Add(statementModel);
        await _context.SaveChangesAsync();
        
        _response.data = statementModel;
        _response.message = "Statement generated successfully";
        _response.statusCode = HttpStatusCode.OK;
        
        return _response;
    }
    
}