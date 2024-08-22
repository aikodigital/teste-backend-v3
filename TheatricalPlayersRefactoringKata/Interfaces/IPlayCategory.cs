public interface IPlayCategory
{
    decimal CalculateAmount(int audience, int lines);
    int CalculateCredits(int audience);
}
