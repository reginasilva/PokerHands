using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using PokerHands.Api.Controllers;
using PokerHands.Api.Models;
using PokerHands.Core.Models;
using PokerHands.Core.Services;
using PokerHands.Core.Utils;

namespace PokerHands.Api.Tests.Unit.Controllers;

public class PokerControllerTests
{
    private readonly PokerController _controller;
    private readonly PokerEvaluatorService _evaluator;
    private readonly DeckService _deckService;

    public PokerControllerTests()
    {
        _evaluator = new PokerEvaluatorService(NullLogger<PokerEvaluatorService>.Instance);
        _deckService = new DeckService();
        _controller = new PokerController(_evaluator, _deckService, NullLogger<PokerController>.Instance);
    }

    private static Hand MakeHand(params string[] cards)
        => HandParser.ParseHand(cards);

    [Fact]
    public void Evaluate_ShouldReturnOk_WhenHandIsValid()
    {
        var hand = new List<string> { "AS", "KS", "QS", "JS", "10S" };
        var result = _controller.EvaluateHand(hand) as OkObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Evaluate_ShouldReturnBadRequest_WhenHandIsNull()
    {
        var result = _controller.EvaluateHand(null!) as BadRequestObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Evaluate_ShouldReturnBadRequest_WhenLessThanFiveCards()
    {
        var hand = new List<string> { "AS", "KS", "QS" };
        var result = _controller.EvaluateHand(hand) as BadRequestObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
    }

    [Fact]
    public void Compare_ShouldReturnOk_WhenAHasHigherHand()
    {
        var request = new CompareRequest
        {
            HandA = new List<string> { "AS", "KS", "QS", "JS", "10S" },
            HandB = new List<string> { "9S", "9D", "9H", "8S", "2C" }
        };

        var result = _controller.CompareHands(request) as OkObjectResult;
        result.Should().NotBeNull();
        result!.Value.Should().NotBeNull();
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public void Compare_ShouldReturnOk_WhenHandsAreEqual()
    {
        var request = new CompareRequest
        {
            HandA = new List<string> { "AS", "KS", "QS", "JS", "10S" },
            HandB = new List<string> { "AS", "KS", "QS", "JS", "10S" }
        };

        var result = _controller.CompareHands(request) as OkObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(200);
        result.Value.ToString().Should().Contain("Tie");
    }

    [Fact]
    public void Compare_ShouldReturnBadRequest_WhenRequestIsNull()
    {
        var result = _controller.CompareHands(null!) as BadRequestObjectResult;
        result.Should().NotBeNull();
        result!.StatusCode.Should().Be(400);
    }
}
