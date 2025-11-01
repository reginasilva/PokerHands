using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a poker hand contains two distinct pairs of cards.
    /// </summary>
    /// <remarks>A "Two Pair"  consists of two cards of one rank,  two cards of another rank, and one card of a different rank.
    /// </remarks>
    public sealed class TwoPairRule : IHandRule
    {
        public HandRank HandRank => HandRank.TwoPair;

        public bool IsMatch(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank);

            int pairCount = groups.Count(g => g.Count() == 2);

            return pairCount == 2;
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var groups = hand.Cards.GroupBy(c => c.Rank).ToList();

            var pairs = groups.Where(g => g.Count() == 2)
                              .Select(g => g.Key)
                              .OrderByDescending(r => r)
                              .ToList();

            var kicker = groups.First(g => g.Count() == 1).Key;

            var tiebreakers = new List<Rank>();
            tiebreakers.AddRange(pairs);
            tiebreakers.Add(kicker);

            return new HandEvaluationResult(HandRank, tiebreakers);
        }
    }
}
