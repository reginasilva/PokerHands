using Microsoft.Extensions.Logging;
using PokerHands.Core.Enums;
using PokerHands.Core.Exceptions;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;

namespace PokerHands.Core.Services
{
    public class PokerEvaluatorService(ILogger<PokerEvaluatorService> logger) : IEvaluatorService
    {
        /// <summary>
        /// Represents the collection of poker hand evaluation rules used to determine the rank of a hand.
        /// </summary>
        /// <remarks>
        /// Don't change the order of the rules, as it defines the priority of evaluation.
        /// </remarks>
        private readonly IEnumerable<IHandRule> _rules =
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
                logger.LogError("Invalid hand: {Cards}", string.Join(", ", hand.Cards));

                throw new InvalidCardException("Hand contains invalid cards.");
            }

            logger.LogDebug("Evaluating hand: {Cards}", string.Join(", ", hand.Cards));

            foreach (var rule in _rules)
            {
                if (rule.IsMatch(hand))
                {
                    var result = rule.Evaluate(hand);

                    logger.LogInformation("Matched rule {Rule} -> {Rank}", rule.GetType().Name, result.HandRank);

                    return result;
                }
            }

            logger.LogWarning("No rule matched hand: {Cards}", string.Join(", ", hand.Cards));

            return new HandEvaluationResult(HandRank.Undefined, hand.Cards.Select(c => c.Rank));
        }
    }
}
