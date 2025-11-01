namespace PokerHands.Core.Enums
{
    /// <summary>
    /// Represents the rank of a poker hand in a standard deck of cards.
    /// </summary>
    public enum HandRank
    {
        Undefined = 0,

        /// <summary>
        /// Represents a poker hand where no other hand combinations are met and the value of the hand is determined by the highest card.
        /// </summary>
        HighCard = 1,

        /// <summary>
        /// Represents a poker hand containing a pair of two related values.
        /// </summary>
        Pair,

        /// <summary>
        /// Represents a poker hand containing two pairs of cards with the same rank.
        /// </summary>
        TwoPair,

        /// <summary>
        /// Represents a poker hand where three cards of the same rank are present.
        /// </summary>
        ThreeOfKind,

        /// <summary>
        /// Represents a poker hand where five cards in sequence, but not of the same suit, are present.
        /// </summary>
        Straight,

        /// <summary>
        /// Represents a poker hand where all five cards are of the same suit, but not in sequence.
        /// </summary>
        Flush,

        /// <summary>
        /// Represents a poker hand where three cards of one rank and two cards of another rank are present.
        /// </summary>
        FullHouse,

        /// <summary>
        /// Represents a poker hand where four cards of the same rank are present.
        /// </summary>
        FourOfKind,

        /// <summary>
        /// Represents a poker hand where five cards in sequence and of the same suit are present.
        /// </summary>
        StraightFlush,

        /// <summary>
        /// Represents a poker hand where the hand contains the Ace, King, Queen, Jack, and Ten all of the same suit.
        /// </summary>
        RoyalFlush
    }
}
