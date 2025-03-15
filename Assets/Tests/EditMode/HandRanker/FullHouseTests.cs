using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class FullHouseTests
    {
        [Test]
        public void TestBestFullHouse()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Six, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Spades),
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.FullHouse);
        }

        [Test]
        public void TestApplicableFullHouse()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts));
            }, HandType.FullHouse, iterations: 1);
        }
    }
}
