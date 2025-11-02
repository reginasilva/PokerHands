using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class StraightRuleTests
{
    private readonly StraightRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenSequential()
    {
        var hand = MakeHand("5D", "6C", "7H", "8S", "9H");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNotSequential()
    {
        var hand = MakeHand("5D", "6C", "7H", "9S", "10H");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnStraightRankAndRanks()
    {
        var hand = MakeHand("5D", "6C", "7H", "8S", "9H");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.Straight);
        result.Tiebreakers.Should().ContainInOrder(Rank.Nine, Rank.Eight, Rank.Seven, Rank.Six, Rank.Five);
    }
}
