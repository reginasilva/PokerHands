using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using PokerHands.Core.Enums;
using PokerHands.Core.Models;
using PokerHands.Core.Services;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Services;

public class PokerEvaluatorServiceTests
{
    private readonly PokerEvaluatorService _evaluator;

    public PokerEvaluatorServiceTests()
    {
        _evaluator = new PokerEvaluatorService(NullLogger<PokerEvaluatorService>.Instance);
    }

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void Evaluate_ShouldIdentifyRoyalFlush()
    {
        var hand = MakeHand("AS", "KS", "QS", "JS", "10S");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.RoyalFlush);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyStraightFlush()
    {
        var hand = MakeHand("9H", "8H", "7H", "6H", "5H");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.StraightFlush);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyFourOfKind()
    {
        var hand = MakeHand("9C", "9D", "9H", "9S", "2C");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.FourOfKind);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyFullHouse()
    {
        var hand = MakeHand("3S", "3H", "3D", "8C", "8S");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.FullHouse);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyFlush()
    {
        var hand = MakeHand("2H", "6H", "9H", "JH", "QH");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.Flush);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyStraight()
    {
        var hand = MakeHand("5D", "6C", "7H", "8S", "9H");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.Straight);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyThreeOfKind()
    {
        var hand = MakeHand("7S", "7D", "7C", "2H", "9S");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.ThreeOfKind);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyTwoPair()
    {
        var hand = MakeHand("4H", "4S", "9D", "9C", "2S");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.TwoPair);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyPair()
    {
        var hand = MakeHand("2C", "2D", "7H", "9S", "10C");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.Pair);
    }

    [Fact]
    public void Evaluate_ShouldIdentifyHighCard()
    {
        var hand = MakeHand("2C", "5H", "7D", "9S", "10C");
        _evaluator.Evaluate(hand).HandRank.Should().Be(HandRank.HighCard);
    }

    [Fact]
    public void CompareHands_ShouldReturn1_WhenFirstWins()
    {
        var strong = MakeHand("AS", "KS", "QS", "JS", "10S");
        var weak = MakeHand("2C", "3D", "4H", "5S", "7C");
        _evaluator.CompareHands(strong, weak).Should().Be(1);
    }

    [Fact]
    public void CompareHands_ShouldReturnMinus1_WhenSecondWins()
    {
        var weak = MakeHand("2C", "3D", "4H", "5S", "7C");
        var strong = MakeHand("AS", "KS", "QS", "JS", "10S");
        _evaluator.CompareHands(weak, strong).Should().Be(-1);
    }

    [Fact]
    public void CompareHands_ShouldReturn0_WhenHandsAreEqual()
    {
        var a = MakeHand("2C", "3D", "4H", "5S", "7C");
        var b = MakeHand("2C", "3D", "4H", "5S", "7C");
        _evaluator.CompareHands(a, b).Should().Be(0);
    }

    [Fact]
    public void Constructor_ShouldRegisterAllTenRulesInCorrectOrder()
    {
        var field = typeof(PokerEvaluatorService)
            .GetField("_rules", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        field.Should().NotBeNull("the evaluator must have a private field '_rules'");

        var rules = field!.GetValue(_evaluator) as IEnumerable<object>;
        
        rules.Should().NotBeNull("the evaluator must initialize its rules list");

        var list = rules!.ToList();
        list.Should().HaveCount(10, "there must be exactly ten hand ranking rules");

        var expectedOrder = new[]
        {
            "RoyalFlushRule",
            "StraightFlushRule",
            "FourOfKindRule",
            "FullHouseRule",
            "FlushRule",
            "StraightRule",
            "ThreeOfKindRule",
            "TwoPairRule",
            "PairRule",
            "HighCardRule"
        };

        var actualOrder = list.Select(r => r.GetType().Name).ToList();

        actualOrder.Should().ContainInOrder(expectedOrder);
    }

}