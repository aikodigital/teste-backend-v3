using TP.Domain.Entities;

public class PlayCalculatorFactory
{
    public static IPlayCalculator CreateCalculator(Play play)
    {
        return play.Type switch
        {
            //TODO:mudar depois para um enum, n é muito recomendado utilizar string desse jeito.
            "tragedy" => new TragedyCalculator(),
            "comedy" => new ComedyCalculator(),
            "history" => new HistoryCalculator(),
            _ => throw new Exception($"Unknown play type: {play.Type}")
        };
    }
}