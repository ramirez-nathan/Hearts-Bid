using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class ThreeKindHelper
    {
        [Test]
        public void TestBestThreeKind()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Nine, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Nine, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Nine, Suit.Diamonds),
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.ThreeOfKind);
        }

        [Test]
        public void TestApplicableThreeKind()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.King, Suit.Clubs));
                hand.Add(CardUtilities.MakeTempCard(Rank.King, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.King, Suit.Hearts));
                HandRankerTestHelper.AddRandomCards(hand, 2);
            }, HandType.ThreeOfKind);
        }
    }
}
