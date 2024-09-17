using Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shared.Commands
{
    public interface ICommandResult<TDTO>
    {
        bool Success { get; }
        string Message { get; }
        ICollection<Error> Errors { get; }
        public TDTO Dto { get; set; }

    }
}
