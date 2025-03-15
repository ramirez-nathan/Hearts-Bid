using HandRankerTests;
using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    public class RoyalFlushTests
    {
        [Test]
        public void TestBestRoyalFlush()
        {
            List<Card> clubs = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.King, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Clubs),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Clubs),
            };
            HandRankerTestHelper.RunBestHandTest(clubs, HandType.RoyalFlush);

            List<Card> spades = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.King, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Spades),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Spades),
            };
            HandRankerTestHelper.RunBestHandTest(spades, HandType.RoyalFlush);

            List<Card> hearts = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.King, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Hearts),
            };
            HandRankerTestHelper.RunBestHandTest(hearts, HandType.RoyalFlush);

            List<Card> diamonds = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.King, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Queen, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Jack, Suit.Diamonds),
                CardUtilities.MakeTempCard(Rank.Ten, Suit.Diamonds),
            };
            HandRankerTestHelper.RunBestHandTest(diamonds, HandType.RoyalFlush);
        }

        [Test]
        public void TestApplicableRoyalFlush()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Ace, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.King, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Queen, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Jack, Suit.Hearts));
                hand.Add(CardUtilities.MakeTempCard(Rank.Ten, Suit.Hearts));
            }, HandType.RoyalFlush, iterations: 1);
        }
    }
}