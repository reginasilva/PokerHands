using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;

namespace PokerHands.Core.Tests.Models;

public class CardTests
{
    [Fact]
    public void ToString_ShouldReturnFullName()
    {
        var card = new Card(Rank.Ace, Suit.Spades);
        card.ToString().Should().Be("Ace of Spades");
    }

    [Fact]
    public void Equality_ShouldWorkByRankAndSuit()
    {
        var a = new Card(Rank.Ten, Suit.Hearts);
        var b = new Card(Rank.Jack, Suit.Hearts);
        var c = new Card(Rank.Ten, Suit.Diamonds);

        a.Rank.Should().NotBe(b.Rank);
        a.Suit.Should().Be(b.Suit);
        a.Rank.Should().Be(c.Rank);
        a.Suit.Should().NotBe(c.Suit);
    }

    [Fact]
    public void CompareTo_ShouldReturnZero_WhenSameRank()
    {
        var a = new Card(Rank.Queen, Suit.Spades);
        var b = new Card(Rank.Queen, Suit.Hearts);

        a.CompareTo(b).Should().Be(0);
        b.CompareTo(a).Should().Be(0);
    }

    [Fact]
    public void CompareTo_ShouldReturnPositive_WhenFirstCardHigher()
    {
        var a = new Card(Rank.Ace, Suit.Spades);
        var b = new Card(Rank.King, Suit.Spades);

        a.CompareTo(b).Should().BePositive();
    }

    [Fact]
    public void CompareTo_ShouldReturnNegative_WhenFirstCardLower()
    {
        var a = new Card(Rank.Nine, Suit.Clubs);
        var b = new Card(Rank.Jack, Suit.Clubs);

        a.CompareTo(b).Should().BeNegative();
    }

    [Fact]
    public void CompareTo_ShouldOrderCardsCorrectly()
    {
        var cards = new List<Card>
        {
            new(Rank.Two, Suit.Clubs),
            new(Rank.Ace, Suit.Spades),
            new(Rank.King, Suit.Hearts),
            new(Rank.Five, Suit.Diamonds)
        };

        var sorted = cards.OrderBy(c => c).ToList();

        sorted.First().Rank.Should().Be(Rank.Two);
        sorted.Last().Rank.Should().Be(Rank.Ace);
    }
}
