using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Enums {
    public enum ErrorType {
        Failure = 0,
        BusinessRule = 1,
        Validation = 2,
        NotFound = 3,
        Conflict = 4,
    }
}
