using FluentAssertions;
using PokerHands.Core.Services;

namespace PokerHands.Core.Tests.Services;

public class DeckServiceTests
{
    private readonly DeckService _deck = new();

    [Fact]
    public void DealHand_ShouldReturnFiveCards()
    {
        var hand = _deck.DealHand();

        hand.Should().NotBeNull();
        hand.Cards.Should().HaveCount(5);
    }

    [Fact]
    public void DealHand_ShouldReturnDifferentHandsByToStringComparison()
    {
        var first = _deck.DealHand().ToString();
        var second = _deck.DealHand().ToString();

        first.Should().NotBeEquivalentTo(second);
    }

    [Fact]
    public void DealHand_ShouldContainOnlyValidCards()
    {
        var hand = _deck.DealHand();

        hand.Cards.Should().OnlyContain(c => c.IsValid);
    }

    [Fact]
    public void Shuffle_ShouldNotThrow()
    {
        var hand = _deck.DealHand();

        hand.Should().NotBeNull();

        Action act = () => _deck.Shuffle(hand.Cards.ToList());
        act.Should().NotThrow();
    }
}
