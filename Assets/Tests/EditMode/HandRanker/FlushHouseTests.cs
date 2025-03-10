using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class FlushHouseTests
    {
        [Test]
        public void TestBestFlushHouse()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Two, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Clubs),
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.FlushHouse);
        }

        [Test]
        public void TestApplicableFlushHouse()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Nine, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Nine, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Three, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Three, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Three, Suit.Hearts));
            }, HandType.FlushHouse, iterations: 1);
        }
    }
}