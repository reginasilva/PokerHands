using PokerHands.Core.Models;

namespace PokerHands.Core.Interfaces
{
    /// <summary>
    /// Defines operations related to deck creation and dealing cards.
    /// </summary>
    public interface IDeckService
    {
        /// <summary>
        /// Creates a standard 52 card deck.
        /// </summary>
        List<Card> CreateDeck();

        /// <summary>
        /// Shuffles a deck randomly.
        /// </summary>
        List<Card> Shuffle(List<Card> deck);

        /// <summary>
        /// Deals a random 5 card hand.
        /// </summary>
        Hand DealHand();
    }
}
