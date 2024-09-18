using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Handles
{
    public interface ICommandHandler<TComand, TDTO> where TComand : ICommand
    {
        Task<ICommandResult<TDTO>> HandleAsync(TComand command);
    }
}
