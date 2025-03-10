using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using System;
using UnityEngine;
using NUnit.Framework;

namespace HandRankerTests
{
    public static class HandRankerTestHelper
    {
        public static void RunBestHandTest(List<Card> hand, HandType expectedBestHand)
        {
            var result = HandRanker.RankHand(hand);
            Assert.IsTrue(result.BestHand == expectedBestHand,
                    $"Expected best hand to be {expectedBestHand}, but it was not.");
        }

        public static void RunHandTest(
            Action<List<Card>> setupHand,
            HandType expectedHandType,
            int iterations = 1000)
        {
            for (int i = 0; i < iterations; i++)
            {
                List<Card> hand = new();
                setupHand(hand);

                var result = HandRanker.RankHand(hand);
                Assert.IsTrue(result.ApplicableHands.Contains(expectedHandType),
                    $"Expected hand to contain {expectedHandType}, but it did not.");
            }
        }

        public static void AddRandomCards(List<Card> hand, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var randomCard = CardUtilities.MakeRandomTempCard();
                hand.Add(randomCard);
            }
        }
    }
}
