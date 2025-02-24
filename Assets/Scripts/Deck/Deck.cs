namespace Scripts.Deck
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Scripts.Card;

    public class Deck : MonoBehaviour
    {
        public List<Card> AllCards => cardsInDeck.Concat(cardsInDiscard).ToList();
        public bool DeckEmpty => cardsInDeck.Count == 0;

        public readonly Queue<Card> cardsInDeck = new();
        public readonly List<Card> cardsInDiscard = new();

        readonly string cardDataPath = "Cards";

        public void Initialize()
        {
            GatherCards();
            ShuffleCards();
        }

        protected void GatherCards()
        {
            List<Card> cards = Resources.LoadAll<Card>(cardDataPath).ToList();
            Debug.Log(cards.Count);
            foreach (var card in cards)
            {
                cardsInDeck.Enqueue(card);
            }
            Debug.Log($"deck size is {cardsInDeck.Count}");
        }

        // MADE REFERENCING KNUTH SHUFFLE ALGORITHM
        // https://rosettacode.org/wiki/Knuth_shuffle
        public void ShuffleCards()
        {
            List<Card> shuffledCards = AllCards.ToList();

            // Create a shuffled list of cards
            for (int i = AllCards.Count - 1; i > 0; i--)
            {
                int j = UnityEngine.Random.Range(0, i + 1);

                // swap cards
                (shuffledCards[j], shuffledCards[i]) = (shuffledCards[i], shuffledCards[j]);
            }

            // Queue the cards into deck
            cardsInDeck.Clear();
            foreach (var card in shuffledCards)
            {
                cardsInDeck.Enqueue(card);
            }

            // Clear the discard (it was shuffled back in)
            cardsInDiscard.Clear();
        }

        public Card Draw(out bool success)
        {
            success = cardsInDeck.Count > 0;
            if (success)
            {
                return cardsInDeck.Dequeue();
            }
            else
            {
                return null;
            }
        }
        // we dont have a discard ability now, but maybe we can
        // repurpose this method to represent cards that have been thrown/cached onto an enemy?
        public void Discard(Card card)
        {
            if (card == null) throw new ArgumentNullException();

            cardsInDiscard.Add(card);
        }
    }
}
