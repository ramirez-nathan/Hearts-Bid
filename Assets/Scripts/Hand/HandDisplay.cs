using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Hand
{
    using Scripts.Card;

    public class HandDisplay : MonoBehaviour
    {
        [SerializeField] protected List<CardDisplay> cardDisplays;
        [SerializeField] protected Hand hand;

        protected virtual void Start()
        {
            hand?.OnHandChanged.AddListener(DisplayHand);
            DisplayHand(hand);
        }

        
        protected virtual void DisplayHand(Hand hand)
        {
            for (int i = 0; i < cardDisplays.Count; i++)
            {
                if (GetCardOrNull(hand, i) != null)
                {
                    cardDisplays[i].DisplayCard(hand.cards[i]);
                }
                else // null case
                {
                    cardDisplays[i].ClearDisplay();
                }
            }
        }

        protected Card GetCardOrNull(Hand hand, int index)
        {
            if (hand == null) return null;

            if (index < hand.cards.Count)
            {
                return hand.cards[index];
            }

            return null;
        }
    }
}
