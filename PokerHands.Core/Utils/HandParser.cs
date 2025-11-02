using PokerHands.Core.Models;

namespace PokerHands.Core.Utils
{
    public static class HandParser
    {
        public static Hand ParseHand(IEnumerable<string> codes)
           => new(codes.Select(CardParser.ParseCard));
    }
}
