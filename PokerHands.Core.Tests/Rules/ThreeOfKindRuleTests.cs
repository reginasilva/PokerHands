using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class ThreeOfKindRuleTests
{
    private readonly ThreeOfKindRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenThreeOfKind()
    {
        var hand = MakeHand("7S", "7D", "7C", "2H", "9S");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNotThreeOfKind()
    {
        var hand = MakeHand("7S", "7D", "6C", "2H", "9S");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnThreeOfKindRankAndRanks()
    {
        var hand = MakeHand("7S", "7D", "7C", "2H", "9S");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.ThreeOfKind);
        result.Tiebreakers.Should().Contain(Rank.Seven);
        result.Tiebreakers.Should().OnlyContain(r => r != Rank.Undefined);
    }
}
