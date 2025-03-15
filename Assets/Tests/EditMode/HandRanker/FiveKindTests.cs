using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class FiveKindTests
    {
        [Test]
        public void TestBestFiveKind()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts)
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.FiveOfKind);
        }

        [Test]
        public void TestApplicableFourKind()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Spades));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Clubs));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts));
            }, HandType.FiveOfKind, iterations: 1);
        }
    }
}