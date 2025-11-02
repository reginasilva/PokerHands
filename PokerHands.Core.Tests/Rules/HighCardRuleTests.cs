using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class HighCardRuleTests
{
    private readonly HighCardRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenNoOtherRuleMatches()
    {
        var hand = MakeHand("2C", "5H", "7D", "9S", "10C");
        var result = _rule.IsMatch(hand);
        result.Should().BeTrue();
    }

    [Fact]
    public void Evaluate_ShouldReturnHighCardRankAndRanks()
    {
        var hand = MakeHand("2C", "5H", "7D", "9S", "10C");
        var result = _rule.Evaluate(hand);
        result.HandRank.Should().Be(HandRank.HighCard);
        result.Tiebreakers.Should().BeInDescendingOrder();
        result.Tiebreakers.Should().OnlyContain(r => r != Rank.Undefined);
    }
}