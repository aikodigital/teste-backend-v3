using System;

namespace TheatricalPlayersRefactoringKata.Domain
{
    public static class PlayFactory
    {
        public static PlayTemplate CreatePlay(string name, string type)
        {
            return type switch
            {
                "tragedy" => new TragedyPlay(name),
                "comedy" => new ComedyPlay(name),
                "history" => new HistoryPlay(name),
                _ => throw new ArgumentException("Tipo de peça inválido.")
            };
        }
    }
}
