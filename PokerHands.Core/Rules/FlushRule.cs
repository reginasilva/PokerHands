using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a hand qualifies as a flush in a card game.
    /// </summary>
    /// <remarks>
    /// A flush is a hand where all cards share the same suit. 
    /// </remarks>
    public sealed class FlushRule : IHandRule
    {
        public HandRank HandRank => HandRank.Flush;

        public bool IsMatch(Hand hand)
        {
            var firstSuit = hand.Cards.First().Suit;

            return hand.Cards.All(c => c.Suit == firstSuit);
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