using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class TwoPairRuleTests
{
    private readonly TwoPairRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenTwoPairsExist()
    {
        var hand = MakeHand("4H", "4S", "9D", "9C", "2S");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenOnlyOnePair()
    {
        var hand = MakeHand("4H", "4S", "9D", "8C", "2S");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnTwoPairRankAndRanks()
    {
        var hand = MakeHand("4H", "4S", "9D", "9C", "2S");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.TwoPair);
        result.Tiebreakers.Should().ContainInOrder(Rank.Nine, Rank.Four, Rank.Two);
    }
}
