using PokerHands.Core.Enums;

namespace PokerHands.Core.Models
{
    /// <summary>
    /// Represents a playing card with a suit and rank. It's immutable.
    /// </summary>
    public sealed class Card : IComparable<Card>
    {
        public Rank Rank { get; }
        public Suit Suit { get; }

        public Card(Rank rank, Suit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        /// <summary>
        /// Gets a value indicating whether the card has a defined rank and suit.
        /// </summary>
        public bool IsValid => Rank != Rank.Undefined && Suit != Suit.Undefined;

        /// <summary>
        /// Compares the current card to another card based on their rank.
        /// </summary>
        /// <param name="other">The card to compare to the current card.</param>
        /// <returns>A signed integer that indicates the relative order of the cards: <br/>
        /// - A positive value if the current card has a higher rank or if <paramref name="other"/> is null. <br/>
        /// - Zero if the ranks of both cards are equal.  <br/>
        /// - A negative value if the current card has a lower rank.
        /// </returns>
        public int CompareTo(Card? other)
        {
            if (other == null)
            { 
                return 1; 
            }

            return Rank.CompareTo(other.Rank);
        }

        /// <summary>
        /// Returns a string representation of the card in the format "Rank of Suit".
        /// </summary>
        public override string ToString()
        {
            return $"{Rank} of {Suit}";
        }
    }
}