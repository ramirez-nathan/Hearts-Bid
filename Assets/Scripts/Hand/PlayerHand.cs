namespace Scripts.Hand
{
    using Scripts.Card;
    using Scripts.Deck;
    using Scripts.Hand;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;

    public class PlayerHand : Hand
    {
        //readonly Deck deck = new();

        private void Awake()
        {
            deck.Initialize();
        }

        private int selectedCardIndex = 0;
        public int SelectedCardIndex { get; private set; } = 0;
        public readonly UnityEvent<int> OnCardSelected = new();

        private void Start()
        {
            DrawStartingHand();
        }

        private void Update()
        {
            HandleCardSelection();
        }

        private void DrawStartingHand()
        {
            Debug.Log($"we made it here, deck size is {deck.cardsInDeck.Count}");
            for (int i = 0; i < handSize; i++)
            {
                DrawCard();
            }
        }

        private void HandleCardSelection()
        {
            for (int i = 0; i < 5; i++)
            {
                // yes ik im using old input system here but will make an input action once this is functional >:(
                if (Keyboard.current[Key.Digit1 + i].wasPressedThisFrame)
                {
                    SelectedCardIndex = i;
                    OnCardSelected.Invoke(SelectedCardIndex);
                }
            }
        }

        public Card FeedSelectedCard()
        {
            if (GetCardCount() == 0 || SelectedCardIndex >= GetCardCount())
            {
                Debug.Log("Hand is empty, not throwing");
                return null;
            }

            Card selectedCard = GetCard(SelectedCardIndex);
            if (selectedCard != null)
            {
                RemoveCard(SelectedCardIndex);
                DrawCard(); // Draw a new card after throwing
                return selectedCard;
            }

            return null;
        }
    }
}