using Codice.Client.Common.GameUI;

namespace Scripts.Hand
{
    using Codice.CM.Common;
    using Scripts.Card;
    using Scripts.Deck;
    using Scripts.Hand;
    using System;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;
    

    public class EnemyHand : Hand
    {
        [SerializeField] float returnDelay = 0.2f;

        public bool FullCache => cards.Count == handSize;
        public readonly UnityEvent onFullCache = new();

        float currentReturnDelay = 0f;
        
        private void Awake()
        {
            
        }
        public void AddCardToCache(Projectile projectile) //called on card collision with enemy
        {
            Debug.Log("Adding card to cache");
            // if somehow this is full
            // we gotta get our card back somehow
            if (!FullCache) 
            {
                cards.Add(projectile.cardData);
                float copyDelay = currentReturnDelay;
                onFullCache.AddListener(() => projectile.BeginReturnToPlayer(copyDelay));
                currentReturnDelay += returnDelay;
                OnHandChanged.Invoke(this);
            }
            else
            {
                projectile.BeginReturnToPlayer(0); // this only gets called when card collides with enemy initially
                // so if cache was played I think this wouldnt get called
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

                onFullCache.Invoke();
                onFullCache.RemoveAllListeners();
                currentReturnDelay = 0;
                OnHandChanged.Invoke(this);
            }
        }
        public void PlayHandOnEnemy()
        {
            HandRankerResult rankedHand = HandRanker.RankHand(cards);
            int damage = HandRanker.HandTypeToDamage[rankedHand.BestHand];
            string handPlayed = HandRanker.RankHand(cards).BestHand.ToString();
            Debug.Log($"You played {handPlayed} on Enemy");
            cards.Clear();
            gameObject.GetComponent<NavMeshEnemy>().TakeHit(damage);
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