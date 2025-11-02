using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;

namespace PokerHands.Core.Tests.Models;

public class HandTests
{
    private static Hand MakeHand(params (Rank rank, Suit suit)[] cards)
        => new(cards.Select(c => new Card(c.rank, c.suit)).ToList());

    [Fact]
    public void IsValid_ShouldReturnTrue_WhenFiveUniqueCards()
    {
        var hand = MakeHand(
            (Rank.Ace, Suit.Spades),
            (Rank.King, Suit.Hearts),
            (Rank.Queen, Suit.Diamonds),
            (Rank.Jack, Suit.Clubs),
            (Rank.Ten, Suit.Spades)
        );

        hand.IsValid.Should().BeTrue();
    }

    [Fact]
    public void IsValid_ShouldReturnFalse_WhenLessThanFiveCards()
    {
        var cards = new List<Card>
    {
        new(Rank.Ace, Suit.Spades),
        new(Rank.King, Suit.Hearts),
        new(Rank.Queen, Suit.Diamonds),
        new(Rank.Jack, Suit.Clubs)
    };

        Action act = () => new Hand(cards);

        act.Should()
           .Throw<ArgumentException>()
           .WithMessage("It's expected exactly 5 cards.");
    }

    [Fact]
    public void ToString_ShouldListAllCards()
    {
        var hand = MakeHand(
            (Rank.Ace, Suit.Spades),
            (Rank.King, Suit.Hearts),
            (Rank.Queen, Suit.Diamonds),
            (Rank.Jack, Suit.Clubs),
            (Rank.Ten, Suit.Spades)
        );

        var result = hand.ToString();

        result.Should().Contain("Ace of Spades");
        result.Split(", ").Should().HaveCount(5);
    }
}
