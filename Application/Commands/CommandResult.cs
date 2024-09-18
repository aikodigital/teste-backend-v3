using Shared.Commands;
using Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CommandResult<TDTO> : ICommandResult<TDTO>
    {
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
            Errors = new List<Error>();

        }

        public CommandResult(bool success, string message, ICollection<Error> errors)
        {
            Success = success;
            Message = message;
            Errors = errors;

        }


        public bool Success { get; set; }
        public string Message { get; set; }
        public ICollection<Error> Errors { get; set; }
        public TDTO Dto { get; set; }
        public IEnumerable<TDTO> Dtos { get; set; } = [];
    }

}

