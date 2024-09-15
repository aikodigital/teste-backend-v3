﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Application.Interfaces
{
    public interface ICalculateCreditAudience
    {
        decimal CalculateCredit(int audience, string gender);
    }
}
