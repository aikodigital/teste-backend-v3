using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Contracts;

namespace TheatricalPlayersRefactoringKata.Performances;

public class TragedyPerformance : BasePerformance
{
    public override int Audience { get; set; }
    public override IPlay Play { get; set; }
    
    public override decimal CalculateAmmount()
    {
        if (Audience <= 30)
            return 0m;
        return 10.00m * (Audience - 30);
    }

    public override int CalculateCredits()
    {
        return 0;
    }

    public TragedyPerformance(IPlay play, int audience) : base(play, audience)
    {
    }
}
