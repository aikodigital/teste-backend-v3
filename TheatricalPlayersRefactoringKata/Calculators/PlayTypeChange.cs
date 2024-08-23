using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata.Interfaces;

namespace TheatricalPlayersRefactoringKata.Calculators
{
internal class PlayTypeChange
    {
        public ICalculateStrategy calculateType;


        public ICalculateStrategy Change(Play type)
        {
            switch (type.Type)
            {
                case "tragedy":
                    calculateType = new CalculateTragedy();
                    break;
                case "comedy":
                    calculateType = new CalculateComedy();
                    break;
                case "history":
                    calculateType = new CalculateHistory();
                    break;
                default:
                    throw new Exception("unknown type: " + type.Name);
            }

            return calculateType;
        }
    }
}