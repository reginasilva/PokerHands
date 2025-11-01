using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a poker hand contains three cards of the same rank.
    /// </summary>
    /// <remarks>
    ///  A "Three of a Kind" hand consists of three cards of the same rank and two other unrelated cards.
    ///  </remarks>
    public sealed class ThreeOfKindRule : IHandRule
    {
        public HandRank HandRank => HandRank.ThreeOfKind;

        public bool IsMatch(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank);

            return groups.Any(g => g.Count() == 3);
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank).ToList();

            var threeKind = groups.First(g => g.Count() == 3).Key;

            var kickers = groups.Where(g => g.Count() == 1)
                                .Select(g => g.Key)
                                .OrderByDescending(r => r)
                                .ToList();

            var tiebreakers = new List<Rank> { threeKind };
            tiebreakers.AddRange(kickers);

            return new HandEvaluationResult(HandRank, tiebreakers);
        }
    }
}
