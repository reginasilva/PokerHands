using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class FourOfKindRuleTests
{
    private readonly FourOfKindRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenFourOfKind()
    {
        var hand = MakeHand("9C", "9D", "9H", "9S", "2C");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNotFourOfKind()
    {
        var hand = MakeHand("9C", "9D", "9H", "8S", "2C");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnFourOfKindRankAndRanks()
    {
        var hand = MakeHand("9C", "9D", "9H", "9S", "2C");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.FourOfKind);
        result.Tiebreakers.Should().ContainInOrder(Rank.Nine, Rank.Two);
    }
}
