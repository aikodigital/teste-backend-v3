using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.microsservice.Domain.Configuracao.Entities.Interfaces
{
    public interface IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        [Required]
        public long Id { get; set; }
        [Column("DATA_REGISTRO")]
        public DateTime? DataRegistro { get; set; }
        [Column("DATA_ATUALIZACAO")]
        public DateTime? DataAtualizacao { get; set; }
        [Column("STATUS_REGISTRO")]
        public bool? StatusRegistro { get; set; }
    }
}
