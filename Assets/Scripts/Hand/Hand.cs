using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    [SerializeField] protected Deck deck;
    [SerializeField] protected int handSize = 5;

    public virtual void DrawCard() // draws card from deck into hand
    {
        if (cards.Count < 5)
        {
            Card drawnCard = deck.Draw(out bool success);
            if (success)
            {
                Debug.Log("success, we are drawing");
                cards.Add(drawnCard);
            }
        }
    }

    public virtual void RemoveCard(int index) // removes card at index
    {
        if (index >= 0 && index < cards.Count)
        {
            cards.RemoveAt(index);
        }
    }

    public Card GetCard(int index) // returns card at that index
    {
        return (index >= 0 && index < cards.Count) ? cards[index] : null;
    }

    public int GetCardCount() 
    {
        return cards.Count;
    }

    // shows current hand in log since we dont have UI to display it rn
    public void LogHandContents() 
    {
        string handContents = "Current Hand: ";
        if (cards.Count == 0)
        {
            handContents += "Empty";
        }
        else
        {
            for (int i = 0; i < cards.Count; i++)
            {
                handContents += $"[{i + 1}: {cards[i].name}] ";
            }
        }
        Debug.Log(handContents);
    }
}
