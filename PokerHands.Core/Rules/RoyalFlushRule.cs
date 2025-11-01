using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents the rule for identifying a Royal Flush in a poker hand evaluation.
    /// </summary>
    /// <remarks>
    /// A Royal Flush is the highest-ranking hand in poker, consisting of the Ace, King, Queen, Jack,
    /// and Ten of the same suit.
    /// </remarks>
    public sealed class RoyalFlushRule : IHandRule
    {
        public HandRank HandRank => HandRank.RoyalFlush;

        public bool IsMatch(Hand hand)
        {
            var suits = hand.Cards.Select(c => c.Suit).Distinct().ToList();

            if (suits.Count != 1)
            {
                return false;
            }

            var ranks = hand.Cards.Select(c => c.Rank).OrderBy(r => r).ToList();

            var royalRanks = new List<Rank> {
                Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace
            };

            return ranks.SequenceEqual(royalRanks);
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var tiebreakers = hand.Cards
                .Select(c => c.Rank)
                .OrderByDescending(r => r)
                .ToList();

            return new HandEvaluationResult(HandRank, tiebreakers);
        }
    }
}