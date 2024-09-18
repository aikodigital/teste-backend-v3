using System.Collections.Generic;

namespace TheatricalPlayersRefactoringKata;

public class InvoiceModelView
{


    public string Customer { get ; set ; }
    public List<PerformanceModelView> Performances { get ; set ; }

    public InvoiceModelView()
    {

    }

}
