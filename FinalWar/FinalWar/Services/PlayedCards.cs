using FinalWar.Models;

namespace FinalWar.Services
{
    /// <summary>
    /// Compares the played cards
    /// </summary>
    public class PlayedCards
    {
        /// <summary>
        /// Gets the cards that were played and by whom
        /// </summary>
        private Dictionary<string, Card> playedCards;

        /// <summary>
        /// Creates the directory 
        /// </summary>
        public PlayedCards()
        {
            playedCards = new Dictionary<string, Card>();
        }

        /// <summary>
        /// Adds a played card to the dictionary
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="card">The card associated to the player</param>
        public void AddPlayedCard(string name, Card card)
        {

            playedCards.Add(name, card);
        }

        /// <summary>
        /// Compares all the played cards
        /// </summary>
        /// <returns>The player's that won and the cards that they won</returns>
        public (List<string> winningPlayers, List<Card> winningPot) ComparePlayedCards()
        {
            Card winningCard = null;
            List<string> winningPlayers = new List<string>();
            List<Card> winningPot = new List<Card>();

            foreach (var entry in playedCards)
            {
                string playerName = entry.Key;
                Card card = entry.Value;

                winningPot.Add(card); // Add the current card to the pot

                if (winningCard == null || card.Rank > winningCard.Rank)
                {
                    winningPlayers.Clear();
                    winningCard = card;
                    winningPlayers.Add(playerName);
                }
                else if (winningCard.Rank == card.Rank)
                {
                    winningPlayers.Add(playerName);
                }
            }
            playedCards.Clear();
            return (winningPlayers, winningPot);
        }
    }
}
