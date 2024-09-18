using Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands;
using Flunt.Notifications;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.TheaterPlayCommands.CreateInBatchTheaterPlayCommands
{
    public class CreateInBatchTheaterPlayCommand : Notifiable<Notification>, ICommand
    {
        public IEnumerable<CreateTheaterPlayCommand> TheaterPlays { get; set; }
        public void Validade()
        {
            throw new NotImplementedException();
        }
    }
}
