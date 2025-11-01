using PokerHands.Core.Models;

namespace PokerHands.Core.Interfaces
{
    /// <summary>
    /// Defines methods for evaluating and comparing hands.
    /// </summary>
    public interface IEvaluatorService
    {
        /// <summary>
        /// Evaluates the given hand and determines its rank and tiebreakers.
        /// </summary>
        /// <param name="hand">The hand to evaluate, consisting of a collection of cards.</param>
        HandEvaluationResult Evaluate(Hand hand);

        /// <summary>
        /// Compares two hands and determines their relative ranking.
        /// </summary>
        /// <param name="handA">The first hand to compare.</param>
        /// <param name="handB">The second hand to compare.</param>
        /// <returns>
        /// An integer indicating the result of the comparison:<br/>
        /// - A positive value if <paramref name="handA"/> ranks higher than <paramref name="handB"/>.<br/>
        /// - A negative value if <paramref name="handB"/> ranks higher than <paramref name="handA"/>.<br/>
        /// - Zero if both hands have the same rank.
        /// </returns>
        int CompareHands(Hand handA, Hand handB);
    }
}
