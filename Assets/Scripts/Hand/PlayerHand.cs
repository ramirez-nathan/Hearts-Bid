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
        public bool sortByRank = false;
        public bool sortBySuit = false;

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
            //Debug.Log($"we made it here, deck size is {deck.cardsInDeck.Count}");
            for (int i = 0; i < handSize; i++)
            {
                DrawCardToHand();
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
                RefillHandSlot(selectedCardIndex); // Draw a new card after throwing
                return selectedCard;
            }

            return null;
        }
        public void ToggleSortByRank(InputAction.CallbackContext context)
        {
            sortByRank = !sortByRank;
            Debug.Log($"Sort by Rank: {sortByRank}");

            UpdateSortingListeners();
        }

        public void ToggleSortBySuit(InputAction.CallbackContext context)
        {
            sortBySuit = !sortBySuit;
            Debug.Log($"Sort by Suit: {sortBySuit}");

            UpdateSortingListeners();
        }

        private void SortHandByRank(Hand hand)
        {
            cards.Sort((card1, card2) => card1.Rank.CompareTo(card2.Rank));
        }

        private void SortHandBySuit(Hand hand)
        {
            cards.Sort((card1, card2) => card1.Suit.CompareTo(card2.Suit));
        }

        private void SortHandBySuitThenRank(Hand hand)
        {
            cards.Sort((card1, card2) =>
            {
                int suitComparison = card1.Suit.CompareTo(card2.Suit);
                return suitComparison != 0 ? suitComparison : card1.Rank.CompareTo(card2.Rank);
            });
        }
        private void UpdateSortingListeners()
        {
            // Remove all sorting listeners first to prevent duplicates
            OnHandChanged.RemoveListener(SortHandByRank);
            OnHandChanged.RemoveListener(SortHandBySuit);
            OnHandChanged.RemoveListener(SortHandBySuitThenRank);

            // Apply sorting once immediately instead of invoking OnHandChanged recursively
            if (sortBySuit && sortByRank)
            {
                SortHandBySuitThenRank(this);
            }
            else if (sortBySuit)
            {
                SortHandBySuit(this);
            }
            else if (sortByRank)
            {
                SortHandByRank(this);
            }

            // Invoke once to notify UI or any dependent systems
            OnHandChanged.Invoke(this);
        }
    }


}