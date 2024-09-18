using System.ComponentModel.DataAnnotations;

namespace aikodigital_teste_backend_v3.Models
{
    public class PerformanceStatement
    {
        [Key]
        public int Id { get; set; }
        public required string Statement { get; set; }
        public required string Customer { get; set; }
        public required string PlayId { get; set; }
        public required int Audience { get; set; }

        public PerformanceStatement()
        {
        }

        public PerformanceStatement(string statement, string customer, Performance performance)
        {
            Statement = statement;
            Customer = customer;
            PlayId = performance.PlayId;
            Audience = performance.Audience;
        }
    }
}
