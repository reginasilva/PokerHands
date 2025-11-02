using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class FullHouseRuleTests
{
    private readonly FullHouseRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenFullHouse()
    {
        var hand = MakeHand("3S", "3H", "3D", "8C", "8S");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNotFullHouse()
    {
        var hand = MakeHand("3S", "3H", "3D", "8C", "9S");
        var result = _rule.IsMatch(hand);
        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnFullHouseRankAndRanks()
    {
        var hand = MakeHand("3S", "3H", "3D", "8C", "8S");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.FullHouse);
        result.Tiebreakers.Should().ContainInOrder(Rank.Three, Rank.Eight);
    }
}
