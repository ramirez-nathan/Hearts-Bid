using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class FlushTests
    {
        [Test]
        public void TestBestFlush()
        {
            List<Card> clubs = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Four, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Clubs),
            };
            HandRankerTestHelper.RunBestHandTest(clubs, HandType.Flush);

            List<Card> spades = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Four, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Spades),
            };
            HandRankerTestHelper.RunBestHandTest(spades, HandType.Flush);

            List<Card> diamonds = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Four, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Diamonds),
            };
            HandRankerTestHelper.RunBestHandTest(diamonds, HandType.Flush);

            List<Card> hearts = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Four, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Six, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Hearts),
            };
            HandRankerTestHelper.RunBestHandTest(hearts, HandType.Flush);
        }

        [Test]
        public void TestNoneFlush()
        {
            // none isn't allowed for flushes so this should be a high card
            List<Card> none = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.None),
                CardUtilities.MakeTempCard(Rank.Four, Suit.None),
                CardUtilities.MakeTempCard(Rank.Six, Suit.None),
                CardUtilities.MakeTempCard(Rank.Eight, Suit.None),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.None),
            };
            HandRankerTestHelper.RunBestHandTest(none, HandType.HighCard);
        }

        [Test]
        public void TestApplicableFlush()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                // returns any suit except none
                var randSuit = CardUtilities.GetRandomEnumInRange<Suit>((int)Suit.Hearts, (int)Suit.Clubs + 1);

                for (int i = 0; i < 5; i++)
                {
                    var randRank = CardUtilities.GetRandomEnum<Rank>();
                    hand.Add(CardUtilities.MakeTempCard(randRank, randSuit));
                }
            }, HandType.Flush);
        }
    }
}
