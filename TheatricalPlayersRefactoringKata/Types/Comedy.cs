using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Types
{
    public class Comedy : Play
    {
        public Comedy(string name, int lines) : base(name, lines) {
        NumberLines = lines;
        }
        
        protected override double CalculatorValorBase()
        {
            int _lines = Math.Clamp(NumberLines, Play.MinLines, Play.MaxLines);
            return _lines / 10.0;
        }
        public override double CalculatorValor()
        {
            double valorBase = CalculatorValorBase();
            double valueWithAdditionals = valorBase + (Audience * 3.00);

            if (Audience > 20)
            {
                valueWithAdditionals += 100.00 + (Audience - 20) * 5.00;
            }

            return valueWithAdditionals;
        }

        public override int CalculatorCredits()
        {
            int baseCredits = Audience <= 30 ? 0 : (Audience - 30);
            int bonusCredits = Audience > 30 ? Audience / 5 : 0;
            return baseCredits + bonusCredits;
        }
    }
}


