using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.DTOs
{
    public class PrintRequestDto
    {
        [Required]
        public string Customer { get; set; }

        [Required]
        public List<string> PlayNames { get; set; }

        [Required]
        public string Format { get; set; }
    }
}
