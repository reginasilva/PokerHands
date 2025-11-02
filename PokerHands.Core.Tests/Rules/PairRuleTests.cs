using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class PairRuleTests
{
    private readonly PairRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenPairExists()
    {
        var hand = MakeHand("2C", "2D", "7H", "9S", "10C");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNoPair()
    {
        var hand = MakeHand("2C", "3D", "7H", "9S", "10C");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnPairRankAndRanks()
    {
        var hand = MakeHand("2C", "2D", "7H", "9S", "10C");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.Pair);
        result.Tiebreakers.Should().Contain(Rank.Two);
        result.Tiebreakers.Should().OnlyContain(r => r != Rank.Undefined);
    }
}
