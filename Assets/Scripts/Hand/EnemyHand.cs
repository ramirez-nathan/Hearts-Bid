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
            Debug.Log("Adding card to cache");
            // if somehow this is full
            // we gotta get our card back somehow
            if (!FullCache) 
            {
                cards.Add(projectile.cardData); 
            }
            UpdateCardDraw(false, projectile.spriteRenderer);
            LogHandAndRank();
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
                PlayHandOnEnemy();
            }
        }
        public void PlayHandOnEnemy()
        {
            HandRankerResult rankedHand = HandRanker.RankHand(cards);
            int damage = HandRanker.HandTypeToDamage[rankedHand.BestHand];
            string handPlayed = HandRanker.RankHand(cards).BestHand.ToString();
            Debug.Log($"You played {handPlayed} on Enemy");
            ReturnCachedCards();
            gameObject.GetComponent<NavMeshEnemy>().TakeHit(damage);
        }

        public void ReturnCachedCards()
        {
            foreach (var card in cards)
            {
                deck.ReturnToDeck(card);
            }
            // do something here that calls a method to
            // make the cards fly back to player 
            cards.Clear();
            // for now we will just delete
           
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

            string handRank = "Current Best Hand Rank: ";
            handRank += HandRanker.RankHand(cards).BestHand.ToString();
            Debug.Log(handRank);
        }
    }
}