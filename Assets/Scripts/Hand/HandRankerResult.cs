namespace Scripts.Hand
{
    using NUnit.Framework;
    using Scripts.Card;
    using Scripts.Hand;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public class HandRankerResult
    {
        public List<Card> InspectedCards { get; private set; }
        public HandType BestHand { get; set; } = HandType.None;
        public List<HandType> ApplicableHands { get; set; } = new();
        public Dictionary<Rank, int> RankOccurrences { get; private set; }
        public Dictionary<Suit, int> SuitOccurrences { get; private set; }
        public int TotalPlayedChips { get; set; } = 0;

        public HandRankerResult(List<Card> cards)
        {

            InspectedCards = cards;
            RankOccurrences = InitializeOccurrences<Rank>();
            SuitOccurrences = InitializeOccurrences<Suit>();

            // tally how much of each rank & suit there are

            foreach (Card card in cards)
            {
                Tally(RankOccurrences, card.Rank);
                Tally(SuitOccurrences, card.Suit);
            }
        }

        //-- Helper Methods

        private static void Tally<T>(Dictionary<T, int> occurances, T target)
        {
            if (occurances.ContainsKey(target))
            {
                occurances[target]++;
            }
            else
            {
                Debug.LogError($"Unrecognized target \"{target} of type \"{target.GetType()}\".");
            }
        }

        private Dictionary<T, int> InitializeOccurrences<T>()
        {
            Dictionary<T, int> occurrences = new();

            foreach (T target in Enum.GetValues(typeof(T)))
            {
                occurrences[target] = 0;
            }

            return occurrences;
        }
    }
}