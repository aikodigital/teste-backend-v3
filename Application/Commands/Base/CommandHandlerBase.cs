using Flunt.Notifications;
using Shared.Commands;
using Shared.Handles;
using Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Base
{
    public abstract class CommandHandlerBase<TCommand, TDTO> : Notifiable<Notification>, ICommandHandler<TCommand, TDTO> where TCommand : ICommand
    {
        public abstract Task<ICommandResult<TDTO>> HandleAsync(TCommand command);

        protected virtual ICollection<Error> GetErrorsFromNotifications(int errorCode)
        {
            HashSet<Error> errors = new();
            foreach (var error in Notifications)
            {
                errors.Add(new Error(errorCode, error.Key, error.Message));
            }

            return errors;
        }
    }
}
