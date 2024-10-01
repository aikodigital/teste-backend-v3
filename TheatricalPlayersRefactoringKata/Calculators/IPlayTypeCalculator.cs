﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Core;

namespace TheatricalPlayersRefactoringKata.Calculators
{
    public interface IPlayTypeCalculator
    {
        decimal CalculateAmount(Performance performance, Play play);
        decimal CalculateVolumeCredits(Performance performance);
    }
}
