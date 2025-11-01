namespace PokerHands.Core.Models
{
    /// <summary>
    /// Represents a poker hand with 5 cards. It's immutable.
    /// </summary>
    public sealed class Hand
    {
        public IReadOnlyCollection<Card> Cards { get; }

        public Hand(IEnumerable<Card> cards)
        {
            if (cards == null)
            {
                throw new ArgumentNullException("There is no cards");
            }

            var list = cards.ToList();

            if (list.Count != 5)
            {
                throw new ArgumentException("It's expected exactly 5 cards.");
            }

            Cards = list.AsReadOnly();
        }

        /// <summary>
        /// Gets a value indicating whether all cards in the hand are valid.
        /// </summary>
        public bool IsValid => Cards.All(c => c.IsValid);

        /// <summary>
        /// Returns a string representation of the hand, listing all cards separated by commas.
        /// </summary>
        /// <returns>A string representation of the hand. <br/>
        /// If the hand is null, returns "No cards". <br/> If the hand is empty, returns "Empty hand". <br/>
        /// Otherwise, returns a comma-separated list of card representations.</returns>
        public override string ToString()
        {
            if (Cards == null)
            {
                return "No cards";
            }

            if (Cards.Count == 0)
            {
                return "Empty hand";
            }

            return string.Join(", ", Cards.Select(c => c.ToString()));
        } 
    }
}
