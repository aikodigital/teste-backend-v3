using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TheatricalPlayersRefactoringKata.Core.Interfaces;

namespace TheatricalPlayersRefactoringKata.Core.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public string Customer { get; set; }
        public Dictionary<string, Play> Plays { get; set; }
        public List<Performance> Performances { get; set; }

        [JsonIgnore]
        private readonly IServiceProvider _serviceProvider;
        [JsonIgnore]
        private readonly IPerformanceCalculatorFactory _performanceCalculatorFactory;

        public decimal TotalAmount => CalculateTotalAmount();
        public int TotalCredits => CalculateTotalCredits();

        [JsonConstructor]
        public Invoice(string customer, Dictionary<string, Play> plays, List<Performance> performances)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Plays = plays ?? new Dictionary<string, Play>();
            Performances = performances ?? new List<Performance>();
        }

        public Invoice(IServiceProvider serviceProvider, IPerformanceCalculatorFactory performanceCalculatorFactory)
        {
            Plays = new Dictionary<string, Play>();
            Performances = new List<Performance>();
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _performanceCalculatorFactory = performanceCalculatorFactory ?? throw new ArgumentNullException(nameof(performanceCalculatorFactory));
        }

        private decimal CalculateTotalAmount()
        {
            return Performances.Sum(performance =>
            {
                if (Plays.TryGetValue(performance.PlayId.ToString(), out var play))
                {
                    var calculator = _performanceCalculatorFactory.CreateCalculator(play.Genre.ToString());
                    return calculator.CalculatePrice(performance);
                }
                return 0;
            });
        }

        private int CalculateTotalCredits()
        {
            return Performances.Sum(performance =>
            {
                if (Plays.TryGetValue(performance.PlayId.ToString(), out var play))
                {
                    var calculator = _performanceCalculatorFactory.CreateCalculator(play.Genre.ToString());
                    return calculator.CalculateCredits(performance);
                }
                return 0;
            });
        }
    }
}