namespace FinalWar.Models
{
    /// <summary>
    /// An outline of what each hand is
    /// </summary>
    public class Hand
    {
        /// <summary>
        /// Creates a Quene that stores the cards
        /// </summary>
        private Queue<Card> cardQueue { get; }

        /// <summary>
        /// Starts by creating the Queue
        /// </summary>
        public Hand()
        {
            cardQueue = new Queue<Card>();
        }

        /// <summary>
        /// Adds the cards to the hand
        /// </summary>
        /// <param name="card">The card add to the hand</param>
        public void AddCard(Card card)
        {
            cardQueue.Enqueue(card);
        }
        //Foreach card played use this to add to the specific player that wins

        /// <summary>
        /// Says if there are cards in the hand
        /// </summary>
        /// <returns>True if the hand is greater than zero, returns false if not</returns>
        public bool HasCards()
        {
            return cardQueue.Count > 0;
        }

        /// <summary>
        /// Plays a card from the hand
        /// </summary>
        /// <returns>the card in the hand</returns>
        public Card PlayCard()
        {
            return cardQueue.Count > 0 ? cardQueue.Dequeue() : null;
        }

        /// <summary>
        /// Gets the card count of the hand
        /// </summary>
        public int CardCount => cardQueue.Count;
    }
}
