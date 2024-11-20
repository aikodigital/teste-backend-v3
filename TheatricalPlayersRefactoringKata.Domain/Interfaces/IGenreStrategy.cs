using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces
{
    public interface IGenreStrategy
    {
        /// <summary>
        /// Calcula o valor total para a apresentação com base no gênero.
        /// </summary>
        /// <param name="audience">Tamanho da platéia.</param>
        /// <param name="basePrice">Preço base calculado para a peça.</param>
        /// <returns>O valor total a ser cobrado.</returns>
        decimal CalculateCost(int audience, int lines);

        /// <summary>
        /// Calcula os créditos gerados para o cliente com base no gênero.
        /// </summary>
        /// <param name="audience">Tamanho da platéia.</param>
        /// <returns>Total de créditos gerados.</returns>
        int CalculateCredits(int audience);
    }
}
