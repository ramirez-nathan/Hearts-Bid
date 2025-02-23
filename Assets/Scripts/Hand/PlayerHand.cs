using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHand : Hand
{

    private void Awake()
    {
        deck.Initialize();
    }

    private int selectedCardIndex = 0;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] public Transform target;
    [SerializeField] private float projectileMoveSpeed = 5.0f;
   
    

    private void Start()
    {
        deck.Initialize();
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
                selectedCardIndex = i;
                Debug.Log($"Selected Card: {GetCard(selectedCardIndex)?.name}");
            }
        }
    }

    public Card FeedSelectedCard()
    {
        
        if (GetCardCount() == 0 || selectedCardIndex >= GetCardCount())
        {
            Debug.Log("Hand is empty, not throwing");
            return null;
        }
        


        Card selectedCard = GetCard(selectedCardIndex);
        if (selectedCard != null)
        {
            RemoveCard(selectedCardIndex);
            DrawCard(); // Draw a new card after throwing
            return selectedCard;
        }

        return null;
    }
}
