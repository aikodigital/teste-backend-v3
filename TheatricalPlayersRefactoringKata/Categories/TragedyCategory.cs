using System;

namespace TheatricalPlayersRefactoringKata.Categories
{
    public class TragedyCategory : IPlayCategory
    {
        public decimal CalculateAmount(int seats, int performanceId)
        {
            // Implementação específica para TragedyCategory
            // Exemplo: retorno fictício, substitua pela lógica real
            return seats * 4.0m;
        }

        public int CalculatePoints(int seats)
        {
            // Implementação específica para TragedyCategory
            return (int)Math.Floor((decimal)seats / 4);
        }
    }
}
