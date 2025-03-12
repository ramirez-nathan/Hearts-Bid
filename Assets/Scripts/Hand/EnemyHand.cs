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
        [SerializeField] Canvas canvas;
        [SerializeField] GameObject handNameDisplay;
        [SerializeField] GameObject flushAOEPrefab;

        public bool FullCache => cards.Count == handSize;
        public readonly UnityEvent onFullCache = new();

        float currentReturnDelay = 0f;
        private void OnEnable()
        {
            GlobalAbilitySystem.GlobalAbilityBehavior += HandleGlobalAbility;
        }

        private void OnDisable()
        {
            GlobalAbilitySystem.GlobalAbilityBehavior -= HandleGlobalAbility;
        }
        private void Awake()
        {
            
        }

        private void HandleGlobalAbility(GlobalAbilityType ability)
        {
            // Check if the triggered ability is the one this enemy should respond to.
            if (ability == GlobalAbilityType.CallAllHands)
            {
                PlayHandOnEnemy();
                //GlobalAbility.GlobalAbilityBehavior -= HandleGlobalAbility;
            }
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
            }
        }

        public void PlayHandOnEnemy()
        {
            if (cards.Count > 0)
            {
                HandRankerResult rankedHand = HandRanker.RankHand(cards);
                int baseDamage = HandRanker.HandTypeToDamage[rankedHand.BestHand];
                int totalChips = rankedHand.TotalPlayedChips;
                int damage = baseDamage * totalChips; // Multiply total chips by the hand's base damage value

                string handPlayed = rankedHand.BestHand.ToString();
                CheckHand(handPlayed, rankedHand);
                Debug.Log($"You played {handPlayed} on Enemy. Total Chips: {totalChips}, Base Damage: {baseDamage}, Final Damage: {damage}");


                DisplayHandString(rankedHand.BestHand);
                cards.Clear();
                gameObject.GetComponent<NavMeshEnemy>().TakeHit(damage);

                onFullCache.Invoke();
                onFullCache.RemoveAllListeners();
                currentReturnDelay = 0;
                OnHandChanged.Invoke(this);
            }
        }

        private void CheckHand(string handName, HandRankerResult hand)
        {
            switch (handName)
            {
                case "Flush":
                    FlushAOEAbility flushAOEAbility = Instantiate(flushAOEPrefab, transform.position, Quaternion.identity).GetComponent<FlushAOEAbility>();
                    flushAOEAbility.Initialize(hand);
                    break;
            }
        }

        private void DisplayHandString(HandType hand)
        {
            HandNameDisplay display = Instantiate(handNameDisplay, canvas.transform).GetComponent<HandNameDisplay>();
            display.DisplayHand(hand);
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