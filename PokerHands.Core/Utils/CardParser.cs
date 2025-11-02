using PokerHands.Core.Enums;
using PokerHands.Core.Models;

namespace PokerHands.Core.Utils
{
    public static class CardParser
    {
        public static Card ParseCard(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return new Card(Rank.Undefined, Suit.Undefined);
            }

            var rankPart = code[..^1];
            var suitChar = code[^1];

            var rank = ParseRank(rankPart);
            var suit = ParseSuit(suitChar);

            return new Card(rank, suit);
        }

        private static Suit ParseSuit(char suitChar)
        {
            return char.ToUpperInvariant(suitChar) switch
            {
                'H' => Suit.Hearts,
                'D' => Suit.Diamonds,
                'C' => Suit.Clubs,
                'S' => Suit.Spades,
                _ => Suit.Undefined
            };
        }

        private static Rank ParseRank(string rankPart)
        {
            return rankPart.ToUpperInvariant() switch
            {
                "2" => Rank.Two,
                "3" => Rank.Three,
                "4" => Rank.Four,
                "5" => Rank.Five,
                "6" => Rank.Six,
                "7" => Rank.Seven,
                "8" => Rank.Eight,
                "9" => Rank.Nine,
                "10" => Rank.Ten,
                "J" => Rank.Jack,
                "Q" => Rank.Queen,
                "K" => Rank.King,
                "A" => Rank.Ace,
                _ => Rank.Undefined
            };
        }
    }
}
