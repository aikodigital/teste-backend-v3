using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class ReportEntity
    {
        private string _statement { get; set; } = string.Empty;
        private string _name { get; set; } = string.Empty;
        private int _seats { get; set; }
        private decimal  _amount { get; set; }
        private int _credits { get; set; }
        public string Statement { get => _statement; set => _statement = value; }
        public string Name { get => _name; set => _name = value; }
        public int Seats { get => _seats; set => _seats = value; }
        public decimal Amount { get => _amount; set => _amount = value; }
        public int Credits { get => _credits; set => _credits = value; }

    }
}
