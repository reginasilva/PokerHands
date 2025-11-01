using PokerHands.Core.Enums;
using PokerHands.Core.Models;

namespace PokerHands.Core.Interfaces
{
    /// <summary>
    /// Defines a rule that recognizes and evaluates a specific type of poker hand.
    /// </summary>
    public interface IHandRule
    {
        /// <summary>
        /// Gets the rank of the hand in a card game.
        /// </summary>
        HandRank HandRank { get; }

        /// <summary>
        /// Determines whether the specified hand satisfies the matching criteria.
        /// </summary>
        /// <param name="hand">The hand to evaluate against the matching criteria.</param>
        bool IsMatch(Hand hand);

        /// <summary>
        /// Evaluates the given hand and determines its rank and tiebreakers.
        /// </summary>
        HandEvaluationResult Evaluate(Hand hand);
    }
}
