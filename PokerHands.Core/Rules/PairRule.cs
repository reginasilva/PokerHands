using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a hand contains a pair in a card game.
    /// </summary>
    /// <remarks>
    /// A "Pair" typically consists of two cards of the same rank.
    /// </remarks>
    public sealed class PairRule : IHandRule
    {
        public HandRank HandRank => HandRank.Pair;

        public bool IsMatch(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank);

            return groups.Any(g => g.Count() == 2);
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank).ToList();

            var pair = groups.First(g => g.Count() == 2).Key;

            var kickers = groups.Where(g => g.Count() == 1)
                                .Select(g => g.Key)
                                .OrderByDescending(r => r)
                                .ToList();

            var tiebreakers = new List<Rank> { pair };
            tiebreakers.AddRange(kickers);

            return new HandEvaluationResult(HandRank, tiebreakers);
        }
    }
}