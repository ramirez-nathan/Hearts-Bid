using NUnit.Framework;
using Scripts.Card;
using Scripts.Hand;
using System.Collections.Generic;
using UnityEngine;

namespace HandRankerTests
{
    [TestFixture, Category("HandRankerTests")]
    public class NoneTests
    {
        [Test]
        public void TestBestNone()
        {
            List<Card> emptyHand = new();
            HandRankerResult result = HandRanker.RankHand(emptyHand);
            Assert.IsTrue(result.BestHand == HandType.None);
        }
    }
}
