using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating a poker hand as a "High Card" hand.
    /// </summary>
    /// <remarks>
    /// A "High Card" hand is the lowest-ranking hand in poker, where no other hand combinations (such as pairs, straights, or flushes) are present. 
    /// The value of the hand is determined by the highest card, with ties broken by the next highest cards in descending order.
    /// </remarks>
    public sealed class HighCardRule : IHandRule
    {
        public HandRank HandRank => HandRank.HighCard;

        /// <summary>
        /// Always matches as the fallback rule, since every hand has a high card.
        /// </summary>
        public bool IsMatch(Hand hand)
        {
            return true;
        }

        public HandEvaluationResult Evaluate(Hand hand)
        {
            var orderedRanks = hand.Cards
                .Select(c => c.Rank)
                .OrderByDescending(r => r)
                .ToList();

            return new HandEvaluationResult(HandRank, orderedRanks);
        }
    }
}
