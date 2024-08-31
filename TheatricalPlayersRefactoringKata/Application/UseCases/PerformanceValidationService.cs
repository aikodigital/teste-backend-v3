using System.Collections.Generic;
using System;
using TheatricalPlayersRefactoringKata.Application.Services;
using TheatricalPlayersRefactoringKata.Core.Entities;

namespace TheatricalPlayersRefactoringKata.Application.UseCases;

public class PerformanceValidationService : IPerformanceValidationService
{
    public void ValidatePerformances(List<Performance> performances)
    {
        foreach (var performance in performances)
        {
            // Implementar a lógica de validação para cada performance.
            // Exemplo: verificar se o público é maior que zero.
            if (performance.Audience <= 0)
            {
                throw new InvalidOperationException("Audience must be greater than zero.");
            }
        }
    }
}