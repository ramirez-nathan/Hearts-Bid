using UnityEngine;

public class PlayerHand : Hand
{
    readonly Deck deck = new();

    private void Awake()
    {
        deck.Initialize();
    }
}
