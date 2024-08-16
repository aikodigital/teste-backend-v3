﻿using TheatricalPlayersRefactoringKata.Communication.Responses;

namespace TheatricalPlayersRefactoringKata.App.Validations.Invoices.Register;
public interface IGetInvoiceByCustomerValidation
{
    Task<ResponseInvoice> Execute(string name);
}