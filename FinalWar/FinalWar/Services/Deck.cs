using FinalWar.Models;

namespace FinalWar.Services
{
    /// <summary>
    /// Creates the deck of cards
    /// </summary>
    public class Deck
    {
        /// <summary>
        /// Creates a stack of cards
        /// </summary>
        private Stack<Card> deckOfCards;

        /// <summary>
        /// Creates an array of suits for the cards
        /// </summary>
        private string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };

        /// <summary>
        /// Starts the generate deck while making a stack of cards
        /// </summary>
        public Deck()
        {
            deckOfCards = new Stack<Card>();
            GenerateDeck();
        }

        /// <summary>
        /// Generates random cards and adds them to the deck
        /// </summary>
        private void GenerateDeck()
        {
            Random random = new Random();

            for (int i = 0; i < 52; i++)
            {
                Rank[] rank = Enum.GetValues(typeof(Rank)).Cast<Rank>().ToArray();
                Rank randomRank = rank[random.Next(rank.Length)];
                string randomSuit = suits[random.Next(suits.Length)];

                deckOfCards.Push(new Card(randomSuit, randomRank));
            }
        }

        /// <summary>
        /// Gives the cards inside the deck
        /// </summary>
        /// <returns>The cards inside the deck</returns>
        public IEnumerable<Card> GetCards()
        {
            return deckOfCards;
        }
    }
}
