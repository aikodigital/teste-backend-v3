using System.Collections.Generic;
using System;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Infrastructure.Repositories;

public class PlayRepository : IPlayRepository
{
    private readonly Dictionary<string, Play> _plays;

    public PlayRepository(Dictionary<string, Play> plays)
    {
        _plays = plays;
    }

    public Play GetPlayById(string playId)
    {
        if (_plays.TryGetValue(playId, out var play))
        {
            return play;
        }
        throw new Exception($"Play not found: {playId}");
    }
}
