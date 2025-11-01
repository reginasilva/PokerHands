namespace PokerHands.Core.Exceptions
{
    public class InvalidCardException : Exception
    {
        public InvalidCardException(string code) :
            base($"Invalid card code: {code}")
        { }
    }
}
