using System;

namespace TheatricalPlayersRefactoringKata.Categories
{
    public class HistoricalCategory : IPlayCategory
    {
        public decimal CalculateAmount(int seats, int performanceId)
        {
            // Implementação específica para HistoricalCategory
            // Exemplo: retorno fictício, substitua pela lógica real
            return seats * 3.0m;
        }

        public int CalculatePoints(int seats)
        {
            // Implementação específica para HistoricalCategory
            return (int)Math.Floor((decimal)seats / 5);
        }
    }
}
