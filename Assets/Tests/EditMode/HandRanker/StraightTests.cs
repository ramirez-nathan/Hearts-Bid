using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class StraightTests
    {
        [Test]
        public void TestBestRegularStraight()
        {
            List<Card> regular = new()
            {
                CardUtilities.MakeTempCard(Rank.Nine, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Seven, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts),
            };
            HandRankerTestHelper.RunBestHandTest(regular, HandType.Straight);
        }

        [Test]
        public void TestBestAceLow()
        {
            List<Card> aceLow = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Two, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Three, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Four, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Five, Suit.Hearts),
            };
            HandRankerTestHelper.RunBestHandTest(aceLow, HandType.Straight);
        }

        [Test]
        public void TestApplicableStraight()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                // returns a rank between two and ten
                int startRankValue = (int)CardUtilities.GetRandomEnumInRange<Rank>((int)Rank.Two, (int)Rank.Jack);

                for (int i = 0; i < 5; i++)
                {
                    hand.Add(CardUtilities.MakeTempCard((Rank)(startRankValue + i), Suit.None));
                }
            }, HandType.Straight);
        }
    }
}
