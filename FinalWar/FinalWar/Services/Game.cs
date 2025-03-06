using FinalWar.Models;

namespace FinalWar.Services
{
    /// <summary>
    /// Handles all of the game logic
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Creates a new deck to give out cards
        /// </summary>
        private Deck newDeck;

        /// <summary>
        /// Creates a new player hand
        /// </summary>
        public PlayerHands hands;

        /// <summary>
        /// Creates a new set of playedCards
        /// </summary>
        private PlayedCards playedCards;

        /// <summary>
        /// Is a message that the game logic will give the blazor page to print to the screen
        /// </summary>
        public string GameMessage { get; private set; } = string.Empty;

        /// <summary>
        /// A string to save the winner's name
        /// </summary>
        public string Winner { get; private set; } = string.Empty;

        /// <summary>
        /// Check to see if there is a winner.
        /// </summary>
        private void CheckForWinner()
        {
            if (hands.GetAllPlayerHands().Count == 1)
            {
                Winner = hands.GetAllPlayerHands().First().Key; // Set the winner's name
                GameMessage = $"{Winner} wins the game!"; // Update the game message
                PlayedCards.Clear(); // Clear played cards as the game is over
            }
        }

        /// <summary>
        /// Gets the current round's played cards.
        /// </summary>
        public Dictionary<string, Card> GetCurrentRoundPlayedCards()
        {
            return new Dictionary<string, Card>(PlayedCards);
        }

        /// <summary>
        /// Gets the dictionary of cards played by each player during the current round.
        /// </summary>
        public Dictionary<string, Card> PlayedCards { get; private set; } = new Dictionary<string, Card>();

        /// <summary>
        /// Gets the cards left in each player's hands and removes those who don't have cards
        /// </summary>
        public void DisplayPlayersWithCardsLeft()
        {
            GameMessage = ""; // Reset the message for fresh display
            foreach (var player in hands.GetAllPlayerHands())
            {
                string playerName = player.Key;
                int cardsLeft = hands.GetCardsLeft(playerName);
                GameMessage += $"{playerName} has {cardsLeft} card(s) left.\n"; // Update the message with cards left
            }
        }

        /// <summary>
        /// Takes in a list of names to pass to the next methos
        /// </summary>
        /// <param name="names">The list of playernames</param>
        public Game(List<string> names)
        {
            newDeck = new Deck();
            hands = new PlayerHands();
            playedCards = new PlayedCards();
            Start(names);
        }

        /// <summary>
        /// Adds the players then passes the names to the deal cards methods
        /// </summary>
        /// <param name="names">List of player names</param>
        public void Start(List<string> names)
        {
            foreach (string name in names)
            {
                hands.AddPlayer(name); // This will now check for duplicates
            }
            DealCards(names);
        }

        /// <summary>
        /// Deal the cards to each player
        /// </summary>
        /// <param name="playerNames">The list of player names</param>
        public void DealCards(List<string> playerNames)
        {
            int playerIndex = 0;

            foreach (Card card in newDeck.GetCards())
            {
                string currentPlayer = playerNames[playerIndex];

                hands.AddCard(currentPlayer, card);

                playerIndex = (playerIndex + 1) % playerNames.Count;
            }
        }

        /// <summary>
        /// Handles each round of the game(comparing the cards the winners and so on)
        /// </summary>
        public void Rounds()
        {
            // List to track players to be removed
            List<string> playersToRemove = new List<string>();

            PlayedCards playedCards = new PlayedCards();
            PlayedCards.Clear();

            // Each player plays a card
            foreach (var player in hands.GetAllPlayerHands())
            {
                if (player.Value.HasCards())
                {
                    Card playedCard = player.Value.PlayCard();
                    playedCards.AddPlayedCard(player.Key, playedCard);
                    PlayedCards[player.Key] = playedCard; // Store the played card
                }
                else
                {
                    // If the player has no cards, add to removal list
                    playersToRemove.Add(player.Key);
                    GameMessage += $"{player.Key} has run out of cards and is out of the game.\n"; // Inform about the player
                }
            }

            // Remove players who have no cards left
            foreach (var playerName in playersToRemove)
            {
                hands.RemovePlayer(playerName); // Assuming you have a RemovePlayer method
            }

            // Check if there are remaining players before comparing cards
            if (hands.GetAllPlayerHands().Count == 0)
            {
                GameMessage = "All players are out of cards. The game is over.";
                return; // Exit the method if no players remain
            }

            // Compare played cards to determine the winner or tied players
            var (winningPlayers, winningPot) = playedCards.ComparePlayedCards();

            // Check if there's a tie
            while (winningPlayers.Count > 1)
            {
                GameMessage = "It's a tie! The following players will play again: " + string.Join(", ", winningPlayers);
                PlayedCards.Clear(); // Clear the previous round cards

                // Each tied player plays again
                foreach (var playerName in winningPlayers)
                {
                    var playerHand = hands.GetHand(playerName);
                    if (playerHand != null && playerHand.HasCards())
                    {
                        Card playedCard = playerHand.PlayCard();
                        playedCards.AddPlayedCard(playerName, playedCard);
                        PlayedCards[playerName] = playedCard; // Store the played card
                        winningPot.Add(playedCard); // Add the new card to the pot
                    }
                }

                // Compare again
                (winningPlayers, winningPot) = playedCards.ComparePlayedCards();
            }

            // If there's a single winning player, add the winning pot to their hand
            if (winningPlayers.Count == 1)
            {
                string winningPlayer = winningPlayers.First();
                foreach (var card in winningPot)
                {
                    hands.AddCard(winningPlayer, card); // Add each card in the pot to the winning player's hand
                }
                GameMessage = $"Player {winningPlayer} wins this round!";
            }
            CheckForWinner();
        }
    }
    
}
