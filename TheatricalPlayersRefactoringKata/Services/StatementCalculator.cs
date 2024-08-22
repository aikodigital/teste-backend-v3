using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementCalculator
    {
        private readonly Dictionary<string, IPlayCategory> _playCategories;
        private readonly Dictionary<string, Play> _plays;

        public StatementCalculator(Dictionary<string, IPlayCategory> playCategories, Dictionary<string, Play> plays)
        {
            _playCategories = playCategories;
            _plays = plays;
        }

        public IPlayCategory GetPlayCategory(string category)
        {
            if (_playCategories.TryGetValue(category, out var playCategory))
            {
                return playCategory;
            }

            throw new KeyNotFoundException($"Play category '{category}' not found.");
        }
    }
}
