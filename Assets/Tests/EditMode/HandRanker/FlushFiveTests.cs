using HandRankerTests;
using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class FlushFiveTests
    {
        [Test]
        public void TestBestFlushFive ()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades)
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.FlushFive);
        }

        [Test]
        public void TestApplicableFlushFive()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Five, Suit.Diamonds));
            }, HandType.FlushFive, iterations: 1);
        }
    }
}