using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Types
{
    public class Tragedy : Play
    {
        public Tragedy(string name, int lines) : base(name, lines) { }
        protected override double CalculatorValorBase()
        {
            int lines = Math.Clamp(NumberLines, Play.MinLines, Play.MaxLines);
            return lines / 10.0;
        }

        public override double CalculatorValor()
        {
            double valorBase = CalculatorValorBase();
            if (Audience <= 30)
            {
                return valorBase;
            }
            return valorBase  + (Audience - 30) * 10.00;
        }
    }
}
