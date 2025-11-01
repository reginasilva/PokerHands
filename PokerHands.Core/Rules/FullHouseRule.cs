using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a poker hand qualifies as a Full House.
    /// </summary>
    /// <remarks>
    /// A Full House is a poker hand containing three cards of one rank and two cards of another rank.
    /// </remarks>
    public sealed class FullHouseRule : IHandRule
    {
        public HandRank HandRank => HandRank.FullHouse;

        public bool IsMatch(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank).ToList();

            bool hasThree = groups.Any(g => g.Count() == 3);
            bool hasPair = groups.Any(g => g.Count() == 2);

            return hasThree && hasPair;
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank).ToList();

            var threeKind = groups.First(g => g.Count() == 3).Key;
            var pair = groups.First(g => g.Count() == 2).Key;

            var tiebreakers = new List<Rank> { threeKind, pair };

            return new HandEvaluationResult(HandRank, tiebreakers);
        }
    }
}
