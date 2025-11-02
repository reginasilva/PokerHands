using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class StraightFlushRuleTests
{
    private readonly StraightFlushRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenStraightFlush()
    {
        var hand = MakeHand("9H", "8H", "7H", "6H", "5H");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNotStraightFlush()
    {
        var hand = MakeHand("9H", "8H", "7H", "6H", "5S");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnStraightFlushRankAndRanks()
    {
        var hand = MakeHand("9H", "8H", "7H", "6H", "5H");
        var result = _rule.Evaluate(hand);

        result.HandRank.Should().Be(HandRank.StraightFlush);
        result.Tiebreakers.Should().ContainInOrder(Rank.Nine, Rank.Eight, Rank.Seven, Rank.Six, Rank.Five);
    }
}
