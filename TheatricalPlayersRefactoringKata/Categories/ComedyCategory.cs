using System;

namespace TheatricalPlayersRefactoringKata.Categories
{
    public class ComedyCategory : IPlayCategory
    {
        public decimal CalculateAmount(int seats, int performanceId)
        {
            // Implementação específica para ComedyCategory
            // Exemplo: retorno fictício, substitua pela lógica real
            return seats * 2.5m;
        }

        public int CalculatePoints(int seats)
        {
            // Implementação específica para ComedyCategory
            return (int)Math.Floor((decimal)seats / 3) + (int)Math.Floor((decimal)seats / 5);
        }
    }
}
