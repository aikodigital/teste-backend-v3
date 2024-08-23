using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Application.DTO;
using TheatricalPlayersRefactoringKata.Application.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.Common.Result;
using TheatricalPlayersRefactoringKata.Domain.Core.Interfaces.IServices;
using TheatricalPlayersRefactoringKata.Domain.Enums;

namespace TheatricalPlayersRefactoringKata.Application.Services {
    public class ApplicationServicePrinter : IApplicationServicePrinter {

        //private readonly TheaterContext _dbContext;
        private readonly IStatementPrinterService _service;
        private readonly IMapper mapper;
        public ApplicationServicePrinter(IStatementPrinterService _service, IMapper mapper) {
            this._service = _service;
            this.mapper = mapper;
        }
        public Result<string> PrintText(InvoiceDTO invoiceDTO, Dictionary<string, PlayDTO> playsDTO) {
            var invoice = mapper.Map<Invoice>(invoiceDTO);
            var plays = mapper.Map<Dictionary<string, Play>>(playsDTO);

            Result<string> invoiceText = _service.PrintText(invoice, plays);

            if (invoiceText.IsSuccess) {
                return Result<string>.Success(invoiceText.Value);
            }

            return Result<string>.Failure(Error.Failure("An error occurred when trying to perform this service", ErrorType.Failure.ToString()), "");
        }
    }
}
