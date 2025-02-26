namespace Scripts.Hand
{
    using Codice.CM.Common;
    using Scripts.Card;
    using Scripts.Deck;
    using Scripts.Hand;
    using UnityEngine;
    using UnityEngine.InputSystem;
    

    public class EnemyHand : Hand
    {
        public bool FullCache => cards.Count == handSize;
        
        private void Awake()
        {
            
        }
        public void AddCardToCache(Projectile projectile)
        {
            // if somehow this is full
            // we gotta get our card back somehow
            if (!FullCache) 
            {
                cards.Add(projectile.cardData); 
            }
            UpdateCardDraw(false, projectile.spriteRenderer);
        }
        private void UpdateCardDraw(bool draw, SpriteRenderer sprite)
        {
            if (draw == false)
            {
                sprite.enabled = false;
                // if this breaks the game
                // then just make opacity 0
            }
            else
            {
                sprite.enabled = true;
            }
        }
        private void LateUpdate()
        {
            if (FullCache)
            {
                // play hand here
            }
        }
        public void LogHandAndRank()
        {
            string handContents = "Current Enemy Cache: ";
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
            string handRank = "Current Hand Rank: ";

        }
    }
}