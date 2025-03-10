using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class TwoPairTests
    {
        [Test]
        public void TestBestTwoPair()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Two, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Two, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades)
            };

            HandRankerTestHelper.RunBestHandTest(hand, HandType.TwoPair);
        }

        [Test]
        public void TestApplicableTwoPair()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Seven, Suit.Clubs));
                hand.Add(CardUtilities.MakeTempCard(Rank.Seven, Suit.Diamonds));
                hand.Add(CardUtilities.MakeTempCard(Rank.Eight, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Eight, Suit.Diamonds));
                HandRankerTestHelper.AddRandomCards(hand, 1);
            }, HandType.TwoPair);
        }
    }
}