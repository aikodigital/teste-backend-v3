using System;
using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.Request
{
    public class InvoiceRequest
    {
        public Invoice Invoice { get; set; }
        public Dictionary<Guid, Play> Plays { get; set; }
    }
}