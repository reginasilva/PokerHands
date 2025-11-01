using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Rules
{
    /// <summary>
    /// Represents a rule for evaluating whether a poker hand is a Straight Flush.
    /// </summary>
    /// <remarks>
    /// A Straight Flush is a hand where all cards are of the same suit and their ranks form a
    /// consecutive sequence.
    /// </remarks>
    public sealed class StraightFlushRule : IHandRule
    {
        public HandRank HandRank => HandRank.StraightFlush;

        public bool IsMatch(Hand hand)
        {
            bool isFlush = hand.Cards.All(c => c.Suit == hand.Cards.First().Suit);

            if (!isFlush)
            {
                return false;
            }

            var ordered = hand.Cards.Select(c => (int)c.Rank).OrderBy(r => r).ToList();

            for (int i = 1; i < ordered.Count; i++)
            {
                if (ordered[i] != ordered[i - 1] + 1)
                {
                    return false;
                }
            }

            return true;
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