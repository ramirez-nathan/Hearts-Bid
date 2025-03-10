using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class StraightFlushTests
    {
        [Test]
        public void TestBestRegularStraightFlush()
        {
            List<Card> regular = new()
            {
                CardUtilities.MakeTempCard(Rank.Nine, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Seven, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts),
            };
            HandRankerTestHelper.RunBestHandTest(regular, HandType.StraightFlush);
        }

        [Test]
        public void TestBestAceLowSF()
        {
            List<Card> aceLow = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Two, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Three, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Four, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds),
            };
            HandRankerTestHelper.RunBestHandTest(aceLow, HandType.StraightFlush);
        }

        [Test]
        public void TestApplicableStraightFlush()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                // returns a rank between two and ten
                int startRankValue = (int)CardUtilities.GetRandomEnumInRange<Rank>((int)Rank.Two, (int)Rank.Jack);
                var randSuit = CardUtilities.GetRandomEnumInRange<Suit>((int)Suit.Hearts, (int)Suit.Clubs + 1);

                for (int i = 0; i < 5; i++)
                {
                    hand.Add(CardUtilities.MakeTempCard((Rank)(startRankValue + i), randSuit));
                }
            }, HandType.StraightFlush);
        }
    }
}