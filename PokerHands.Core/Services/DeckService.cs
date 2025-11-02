using PokerHands.Core.Enums;
using PokerHands.Core.Interfaces;
using PokerHands.Core.Models;

namespace PokerHands.Core.Services
{
    public class DeckService : IDeckService
    {
        private readonly Random _random = new();

        /// <summary>
        /// Creates a standard deck of 52 unique cards.
        /// </summary>
        public List<Card> CreateDeck()
        {
            var deck = new List<Card>();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                if (suit == Suit.Undefined)
                    continue;

                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    if (rank == Rank.Undefined)
                        continue;

                    deck.Add(new Card(rank, suit));
                }
            }

            return deck;
        }

        /// <summary>
        /// Randomly shuffles the deck.
        /// </summary>
        public List<Card> Shuffle(List<Card> deck)
        {
            return deck.OrderBy(_ => _random.Next()).ToList();
        }

        /// <summary>
        /// Deals a random 5-card hand from a freshly shuffled deck.
        /// </summary>
        public Hand DealHand()
        {
            var deck = Shuffle(CreateDeck());

            var hand = deck.Take(5).ToList();

            return new Hand(hand);
        }
    }
}
