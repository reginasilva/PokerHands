using Microsoft.Extensions.Logging;
using PokerHands.Core.Enums;
using PokerHands.Core.Exceptions;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;

namespace PokerHands.Core.Services
{
    public class PokerEvaluatorService : IEvaluatorService
    {
        private readonly IEnumerable<IHandRule> _rules;
        private readonly ILogger<PokerEvaluatorService> _logger;

        public PokerEvaluatorService(ILogger<PokerEvaluatorService> logger)
        {
            _logger = logger;

            // don't change the order of the rules, as it defines the priority of evaluation
            _rules =
                [
                    new RoyalFlushRule(),
                    new StraightFlushRule(),
                    new FourOfKindRule(),
                    new FullHouseRule(),
                    new FlushRule(),
                    new StraightRule(),
                    new ThreeOfKindRule(),
                    new TwoPairRule(),
                    new PairRule(),
                    new HighCardRule()
                ];
        }

        public int CompareHands(Hand handA, Hand handB)
        {
            var evalA = Evaluate(handA);
            var evalB = Evaluate(handB);

            if (evalA.HandRank != evalB.HandRank)
            {
                return evalA.HandRank.CompareTo(evalB.HandRank);
            }

            for (int i = 0; i < Math.Min(evalA.Tiebreakers.Count, evalB.Tiebreakers.Count); i++)
            {
                var comparation = evalA.Tiebreakers[i].CompareTo(evalB.Tiebreakers[i]);

                if (comparation != 0)
                {
                    return comparation;
                }
            }

            return 0;
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            if (!hand.IsValid)
            {
                _logger.LogError("Invalid hand: {Cards}", string.Join(", ", hand.Cards));

                throw new InvalidCardException("Hand contains invalid cards.");
            }

            _logger.LogDebug("Evaluating hand: {Cards}", string.Join(", ", hand.Cards));

            foreach (var rule in _rules)
            {
                if (rule.IsMatch(hand))
                {
                    var result = rule.Evaluate(hand);

                    _logger.LogInformation("Matched rule {Rule} -> {Rank}", rule.GetType().Name, result.HandRank);

                    return result;
                }
            }

            _logger.LogWarning("No rule matched hand: {Cards}", string.Join(", ", hand.Cards));

            return new HandEvaluationResult(HandRank.Undefined, hand.Cards.Select(c => c.Rank));
        }
    }
}
