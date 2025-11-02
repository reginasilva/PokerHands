using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Utils;

public class CardParserTests
{
    [Fact]
    public void ParseCard_ShouldParseValidSingleDigitRank()
    {
        var card = CardParser.ParseCard("2H");

        card.Rank.Should().Be(Rank.Two);
        card.Suit.Should().Be(Suit.Hearts);
    }

    [Fact]
    public void ParseCard_ShouldParseTenCorrectly()
    {
        var card = CardParser.ParseCard("10S");

        card.Rank.Should().Be(Rank.Ten);
        card.Suit.Should().Be(Suit.Spades);
    }

    [Fact]
    public void ParseCard_ShouldIgnoreCase()
    {
        var card = CardParser.ParseCard("qH");

        card.Rank.Should().Be(Rank.Queen);
        card.Suit.Should().Be(Suit.Hearts);
    }

    [Fact]
    public void ParseCard_ShouldThrow_WhenInvalidFormat()
    {
        var card = CardParser.ParseCard("1X");

        card.Rank.Should().Be(Rank.Undefined);
        card.Suit.Should().Be(Suit.Undefined);
    }

    [Fact]
    public void ParseCard_ShouldThrow_WhenNull()
    {
        var card = CardParser.ParseCard(null!);

        card.Rank.Should().Be(Rank.Undefined);
        card.Suit.Should().Be(Suit.Undefined);
    }
}
