namespace PokerHands.Core.Exceptions
{
    public class InvalidCardException(string code) : 
        Exception($"Invalid card code: {code}")
    {
    }
}
