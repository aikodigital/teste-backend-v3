using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class TheaterPlayDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PlayId { get; set; }
        public PlayDTO? Play { get; set; }

        public static implicit operator TheaterPlayDTO(TheaterPlayEntity entity)
        {
            var Dto = new TheaterPlayDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                PlayId = entity.PlayId,
                Play = entity.Play == null ? null : new PlayDTO { Lines = entity.Play.Lines, Name = entity.Play.Name, Type = entity.Play.Type }

            };

            return Dto;
        }
    }
}
