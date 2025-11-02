using FluentAssertions;
using PokerHands.Core.Utils;

namespace PokerHands.Core.Tests.Utils
{
    public class HandParserTests
    {
        [Fact]
        public void ParseHand_ShouldReturnFiveCards()
        {
            var hand = HandParser.ParseHand(["AS", "KS", "QS", "JS", "10S"]);

            hand.Cards.Should().HaveCount(5);
            hand.Cards.Should().OnlyContain(c => c.IsValid);
        }

        [Fact]
        public void ParseHand_ShouldThrow_WhenNullInput()
        {

            Action act = () => HandParser.ParseHand(null!);

            act.Should()
                .Throw<ArgumentNullException>()
                .WithParameterName("source");
        }
    }
}
