using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class HighCardTests
    {
        [Test]
        public void TestBestHighCard()
        {
            List<Card> hand = new()
            {
                CardUtilities.MakeTempCard(Rank.Ace, Suit.Hearts)
            };
            
            HandRankerTestHelper.RunBestHandTest(hand, HandType.HighCard);
        }

        [Test]
        public void TestApplicableHighCard()
        {
            HandRankerTestHelper.RunHandTest(hand =>
            {
                hand.Add(CardUtilities.MakeTempCard(Rank.Ace, Suit.Hearts));
                HandRankerTestHelper.AddRandomCards(hand, 4);
            }, HandType.HighCard);
        }
    }
}