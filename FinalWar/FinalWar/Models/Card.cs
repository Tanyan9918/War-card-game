namespace FinalWar.Models
{
    /// <summary>
    /// Gives outline for each of the cards
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The suit of the card (Spades, Clubs, Diamonds, Hearts)
        /// </summary>
        public string Suit { get; set; }

        /// <summary>
        /// The rank of the card connected to the enum Rank for comparison
        /// </summary>
        public Rank Rank { get; set; }

        /// <summary>
        /// Every card has to have these
        /// </summary>
        /// <param name="suit">The suit of the card(hearts, spades, clubs, diamonds)</param>
        /// <param name="rank"></param>
        public Card(string suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }
}
