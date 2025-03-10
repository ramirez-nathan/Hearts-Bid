using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class PairTests
    {
        [Test]
        public void TestBestPair()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Two, Suit.Clubs)
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.Pair);
        }

        [Test]
        public void TestApplicablePair()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
                HandRankerTestHelper.AddRandomCards(hand, 3);
            }, HandType.Pair);
        }
    }
}
