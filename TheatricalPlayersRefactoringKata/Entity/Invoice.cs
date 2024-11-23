using System;
using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entity;

public class Invoice
{
    public Guid Id{ get; set; }
    public string Customer { get; set; }
    public List<Performance> Performances { get;set ; }
}
