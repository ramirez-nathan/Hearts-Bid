using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField] int handSize = 5;

    public List<Card> cards;

    private void Awake()
    {
        cards = new(handSize);
    }

    /*
    // Abstract method required by UML
    public virtual Card[] GetHand()
    {
        return cards;
    }

    // Optional: Common functionality for all hands
    protected bool IsValidIndex(int index)
    {
        return index >= 0 && index < cards.Length;
    }
    */
}