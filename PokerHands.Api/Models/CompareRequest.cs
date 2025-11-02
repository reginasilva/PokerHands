namespace PokerHands.Api.Models
{
    /// <summary>
    ///  Represents the request body for comparing two hands.
    /// </summary>
    public class CompareRequest
    {
        /// <summary>
        /// The first hand, as a list of string representations of cards (e.g., "AS", "10H", "3D", "KC", "7S").
        /// </summary>
        public List<string> Hand1 { get; set; } = [];

        /// <summary>
        /// The second hand, as a list of string representations of cards (e.g., "2S", "5H", "9D", "QC", "10S").
        /// </summary>
        public List<string> Hand2 { get; set; } = [];
    }
}
