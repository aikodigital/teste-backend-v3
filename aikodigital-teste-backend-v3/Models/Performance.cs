using System.ComponentModel.DataAnnotations;

namespace aikodigital_teste_backend_v3.Models
{
    public class Performance
    {
        [Key]
        public int Id { get; set; }
        public required string PlayId { get; set; } 
        public int Audience { get; set; }

        public Performance() 
        {
        }

        public Performance(string playId, int audience)
        {
            PlayId = playId;
            Audience = audience;
        }
    }
}
