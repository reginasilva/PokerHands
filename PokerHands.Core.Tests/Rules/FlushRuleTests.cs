using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class FlushRuleTests
{
    private readonly FlushRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenAllCardsSameSuit()
    {
        var hand = MakeHand("2H", "5H", "7H", "9H", "JH");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenDifferentSuits()
    {
        var hand = MakeHand("2H", "5H", "7H", "9H", "JC");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnFlushRankAndRanks()
    {
        var hand = MakeHand("2H", "5H", "7H", "9H", "JH");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.Flush);
        result.Tiebreakers.Should().BeInDescendingOrder();
    }
}
