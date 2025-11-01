using PokerHands.Core.Enums;

namespace PokerHands.Core.Models
{
    /// <summary>
    /// Represents the result of evaluating a poker hand, including its rank and any tiebreaker values.
    /// </summary>
    public sealed class HandEvaluationResult
    {
        public HandRank HandRank { get; }
        public IReadOnlyList<Rank> Tiebreakers { get; }

        public HandEvaluationResult(HandRank rank, IEnumerable<Rank> tiebreakers)
        {
            if (tiebreakers == null)
            {
                throw new ArgumentNullException(nameof(tiebreakers), "Tiebreakers cannot be null.");
            }

            HandRank = rank;
            Tiebreakers = tiebreakers.ToList().AsReadOnly();
        }

        /// <summary>
        /// Returns a string representation of hand evaluation result.
        /// </summary>
        /// <returns>A string in the format "Rank (Tiebreaker1, Tiebreaker2, ...)", where <c>Rank</c> is the rank value and
        /// <c>Tiebreakers</c> is a comma-separated list of tiebreaker values.</returns>
        public override string ToString()
        {
            return $"{HandRank} ({string.Join(", ", Tiebreakers)})";
        }
    }
}
