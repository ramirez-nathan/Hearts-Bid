using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class FourKindTests
    {
        [Test]
        public void TestBestFourKind()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Clubs),
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.FourOfKind);
        }

        [Test]
        public void TestApplicableFourKind()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Four, Suit.Spades));
                hand.Add(CardUtilities.MakeTempCard(Rank.Four, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Four, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Four, Suit.Clubs));
                HandRankerTestHelper.AddRandomCards(hand, 1);
            }, HandType.FourOfKind);
        }
    }
}