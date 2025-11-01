using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;
using System.Linq;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a poker hand qualifies as a straight.
    /// </summary>
    /// <remarks>
    /// A straight is a poker hand containing five cards of sequential rank, regardless of suit.
    /// </remarks>
    public sealed class StraightRule : IHandRule
    {
        public HandRank HandRank => HandRank.Straight;

        public bool IsMatch(Hand hand)
        {
            var ordered = hand.Cards
                 .Select(c => c.Rank)
                 .OrderBy(r => r)
                 .ToList();

            bool isAceLow = ordered.SequenceEqual(new List<Rank>
            {
                Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Ace
            });

            if (isAceLow)
            {
                return true;
            }

            bool isSequential = ordered
                .Select((r, i) => (int)r - i)
                .Distinct()
                .Count() == 1;

            return isSequential;
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var ordered = hand.Cards
                .Select(c => c.Rank)
                .OrderByDescending(r => r)
                .ToList();

            return new HandEvaluationResult(HandRank, ordered);
        }
    }
}
