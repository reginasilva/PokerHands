using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Rules;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Rules;

public class RoyalFlushRuleTests
{
    private readonly RoyalFlushRule _rule = new();

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void IsMatch_ShouldReturnTrue_WhenRoyalFlush()
    {
        var hand = MakeHand("AS", "KS", "QS", "JS", "10S");
        var result = _rule.IsMatch(hand);

        result.Should().BeTrue();
    }

    [Fact]
    public void IsMatch_ShouldReturnFalse_WhenNotRoyalFlush()
    {
        var hand = MakeHand("9S", "KS", "QS", "JS", "10S");
        var result = _rule.IsMatch(hand);

        result.Should().BeFalse();
    }

    [Fact]
    public void Evaluate_ShouldReturnRoyalFlushRankAndRanks()
    {
        var hand = MakeHand("AS", "KS", "QS", "JS", "10S");
        var result = _rule.Evaluate(hand);

        result.HandRank.Should().Be(HandRank.RoyalFlush);
        result.Tiebreakers.Should().ContainInOrder(Rank.Ace, Rank.King, Rank.Queen, Rank.Jack, Rank.Ten);
    }
}
