using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class TheaterPlayEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int PlayId { get; set; }
        public PlayEntity? Play { get; set; }

        public static implicit operator TheaterPlayEntity(TheaterPlayDTO dto)
        {
            var entity = new TheaterPlayEntity
            {
                Id = dto.Id,
                Name = dto.Name,
                PlayId = dto.PlayId,
                Play = dto.Play == null ? null : new PlayEntity { Lines = dto.Play.Lines, Name = dto.Play.Name, Type = dto.Play.Type  }
            };

            return entity;
        }
    }
}
