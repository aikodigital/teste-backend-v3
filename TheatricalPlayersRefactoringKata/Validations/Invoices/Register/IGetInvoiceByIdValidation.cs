﻿using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.Validations.Invoices.Register;
public interface IGetInvoiceByIdValidation
{
    Task<ResponseInvoice> Execute(long id);
}