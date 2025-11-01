using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a poker hand qualifies as a "Four of a Kind."
    /// </summary>
    /// <remarks>
    /// A "Four of a Kind" hand contains four cards of the same rank and one additional card (the kicker)
    /// </remarks>
    public sealed class FourOfKindRule : IHandRule
    {
        public HandRank HandRank => HandRank.FourOfKind;

        public bool IsMatch(Hand hand)
        {
            var grouped = hand.Cards.GroupBy(c => c.Rank);

            return grouped.Any(g => g.Count() == 4);
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank).ToList();

            var fourKind = groups.First(g => g.Count() == 4).Key;

            var kicker = groups.First(g => g.Count() == 1).Key;

            var tiebreakers = new List<Rank> { fourKind, kicker };

            return new HandEvaluationResult(HandRank, tiebreakers);
        }
    }
}
