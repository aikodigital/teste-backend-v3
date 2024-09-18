using Domain.DTOs;
using Flunt.Notifications;
using Shared.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace Application.Commands.TheaterPlayCommands.CreateTheaterPlayCommands
{
    public class CreateTheaterPlayCommand : Notifiable<Notification>, ICommand
    {
        public string Name { get; set; }
        public PlayModelView Play { get; set; }
        public void Validade()
        {
            throw new NotImplementedException();
        }

        public TheaterPlayDTO Dto() {


            var  theaterPlayDTO = new TheaterPlayDTO
            {
                Name = Name,
                Id = 0,
            };

            theaterPlayDTO.Play = Play == null ? null : new PlayDTO { Lines = Play.Lines , Name = Play.Name, Type = Play.Type };
            return theaterPlayDTO;

        }
    }
}
