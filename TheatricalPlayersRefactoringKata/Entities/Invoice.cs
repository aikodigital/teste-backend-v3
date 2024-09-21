using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata.Entities;

public class Invoice
{
    public string Customer { get; set; }
    public List<Performance> Performances { get; set; }
}
