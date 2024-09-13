using TP.Domain.Entities;

public interface IPlayCalculator
{
    decimal CalculateAmount(Performance performance);
    int CalculateCredits(Performance performance);
}