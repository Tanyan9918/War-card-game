using FinalWar.Models;

namespace FinalWar.Services
{
    /// <summary>
    /// Handles the playerHands
    /// </summary>
    public class PlayerHands
    {
        /// <summary>
        /// Connects the Hand to a specific player
        /// </summary>
        private Dictionary<string, Hand> playerHand;

        /// <summary>
        /// Creates the dictionary
        /// </summary>
        public PlayerHands()
        {
            playerHand = new Dictionary<string, Hand>();
        }

        public int GetCardsLeft(string name)
        {
            var hand = GetHand(name);
            return hand != null ? hand.CardCount : 0;
        }

        /// <summary>
        /// Adds a player hand for each new player
        /// </summary>
        /// <param name="name">The name of the player</param>
        public void AddPlayer(string name)
        {
            if (!playerHand.ContainsKey(name)) // Check if the player already exists
            {
                playerHand[name] = new Hand();
            }
        }

        /// <summary>
        /// Adds the card to the player 
        /// </summary>
        /// <param name="name">The name of the player</param>
        /// <param name="card">The card being added</param>
        public void AddCard(string name, Card card)
        {
            playerHand[name].AddCard(card);
        }

        /// <summary>
        /// Removes a player
        /// </summary>
        /// <param name="name">The name of the player being removed</param>
        public void RemovePlayer(string name)
        {
            if (playerHand.ContainsKey(name))
            {
                playerHand.Remove(name);
            }
        }

        /// <summary>
        /// Gets all player names in the game
        /// </summary>
        /// <returns>A list of player names</returns>
        public List<string> GetAllPlayerNames()
        {
            return playerHand.Keys.ToList();
        }

        /// <summary>
        /// Plays the card that is associated with the player
        /// </summary>
        /// <param name="name">The name of the player</param>
        public void PlayCard(string name)
        {
            playerHand[name].PlayCard();
        }

        /// <summary>
        /// Gets the all the players hands
        /// </summary>
        /// <returns>the players hands</returns>
        public Dictionary<string, Hand> GetAllPlayerHands()
        {
            return playerHand;
        }

        /// <summary>
        /// Retrieves the hand of a specific player.
        /// </summary>
        /// <param name="name">The name of the player whose hand is to be retrieved.</param>
        /// <returns>The player's hand.</returns>
        public Hand GetHand(string name)
        {
            playerHand.TryGetValue(name, out Hand hand);
            return hand;
        }
    }
}
