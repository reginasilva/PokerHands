using FluentAssertions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;

namespace PokerHands.Core.Tests.Models;

public class HandEvaluationResultTests
{
    [Fact]
    public void ToString_ShouldReturnReadableResult()
    {
        var result = new HandEvaluationResult(HandRank.Flush, new List<Rank> { Rank.Ace, Rank.King, Rank.Jack });

        result.ToString().Should().Contain("Flush");
        result.ToString().Should().Contain("Ace");
    }
}
