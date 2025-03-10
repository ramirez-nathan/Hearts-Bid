 namespace Scripts.Hand
{
    using Scripts.Card;
    using System.Collections.Generic;
    using System.Linq;

    public static class HandRanker
    {
        public static HandRankerResult RankHand(Hand hand)
        {
            return RankHand(hand.cards);
        }

        public static HandRankerResult RankHand(List<Card> cards)
        {
            HandRankerResult result = new(cards);

            // rank based hand types

            if (HasRankCount(result, 1)) result.ApplicableHands.Add(HandType.HighCard);
            if (HasRankCount(result, 2)) result.ApplicableHands.Add(HandType.Pair);
            if (HasRankCount(result, 3)) result.ApplicableHands.Add(HandType.ThreeOfKind);
            if (HasRankCount(result, 4)) result.ApplicableHands.Add(HandType.FourOfKind);
            if (HasRankCount(result, 5)) result.ApplicableHands.Add(HandType.FiveOfKind);

            if (HasTwoPair(result)) result.ApplicableHands.Add(HandType.TwoPair);
            if (HasFullHouse(result)) result.ApplicableHands.Add(HandType.FullHouse);

            if (HasStraight(result)) result.ApplicableHands.Add(HandType.Straight);

            // suit based hand types

            if (HasFlush(result)) result.ApplicableHands.Add(HandType.Flush);
            if (HasFlushHouse(result)) result.ApplicableHands.Add(HandType.FlushHouse);
            if (HasFlushFive(result)) result.ApplicableHands.Add(HandType.FlushFive);
            if (HasStraightFlush(result)) result.ApplicableHands.Add(HandType.StraightFlush);
            if (HasRoyalFlush(result)) result.ApplicableHands.Add(HandType.RoyalFlush);

            // check if any hand types were added
            if (result.ApplicableHands.Count == 0) result.ApplicableHands.Add(HandType.None);

            // select the best hand out of applicable
            result.BestHand = result.ApplicableHands.Max();
            
            result.TotalPlayedChips = SumPlayedChips(result);
            return result;
        }

        private static bool HasRoyalFlush(HandRankerResult result)
        {
            List<Rank> uniqueRanks = GetUniqueRanks(result);

            // directly check if theres a royal flush
            return uniqueRanks.Contains(Rank.Ten) &&
                uniqueRanks.Contains(Rank.Jack) &&
                uniqueRanks.Contains(Rank.Queen) &&
                uniqueRanks.Contains(Rank.King) &&
                uniqueRanks.Contains(Rank.Ace);
        }

        private static List<Rank> GetUniqueRanks(HandRankerResult result)
        {
            // get list of unique ranks in set of cards
            return result.RankOccurrences.Where(kvp =>
            kvp.Key != Rank.None && kvp.Key != Rank.Joker
            && kvp.Value >= 1)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        private static bool HasStraightFlush(HandRankerResult result)
        {
            return HasFlush(result) && HasStraight(result);
        }

        private static bool HasFlushFive(HandRankerResult result)
        {
            return HasFlush(result) && HasRankCount(result, 5);
        }

        private static bool HasFlushHouse(HandRankerResult result)
        {
            return HasFlush(result) && HasFullHouse(result);
        }

        private static bool HasFlush(HandRankerResult result)
        {
            // has any suit collection has 5 or more cards
            return result.SuitOccurrences.Where(kvp =>
            kvp.Key != Suit.None &&
            kvp.Value >= 5).Any();
        }

        private static bool HasStraight(HandRankerResult result)
        {
            // get list of unique ranks in set of cards
            var uniqueRanks = GetUniqueRanks(result);

            // try to find a consequtive set of 5 cards
            for (int i = 0; i < uniqueRanks.Count - 5; i++)
            {
                if (IsConsecutive(uniqueRanks, i))
                {
                    return true;
                }
            }

            // check for ace-low straight
            return uniqueRanks.Contains(Rank.Ace) &&
                uniqueRanks.Contains(Rank.Two) &&
                uniqueRanks.Contains(Rank.Three) &&
                uniqueRanks.Contains(Rank.Four) &&
                uniqueRanks.Contains(Rank.Five);
        }

        private static bool IsConsecutive(List<Rank> uniqueRanks, int i)
        {
            // going to check next 4 cards
            for (int j = 1; j < 5; j++)
            {
                // out of bounds
                if (i + j >= uniqueRanks.Count()) return false;

                // number and next number not consecutive
                if ((int)uniqueRanks[i] != (int)uniqueRanks[i + j])
                {
                    return false;
                }
            }

            // all consecutive
            return true;
        }

        private static bool HasFullHouse(HandRankerResult result)
        {
            // find groups with 2 or more cards
            var groupsOfTwoOrMore = result.RankOccurrences
            .Where(kvp => kvp.Key != Rank.None && kvp.Value >= 2)
            .ToList();

            // has a group with 2 or more and a group with 3 or more not from same rank
            return groupsOfTwoOrMore.Any(g1 =>
                groupsOfTwoOrMore.Any(g2 => g1.Key != g2.Key && g2.Value >= 3));
        }

        private static bool HasTwoPair(HandRankerResult result)
        {
            // has 2 rank occurences with 2 or more cards
            return result.RankOccurrences.Where(kvp =>
            kvp.Key != Rank.None &&
            kvp.Value >= 2)
                .Count() >= 2;
        }

        private static bool HasRankCount(HandRankerResult result, int count)
        {
            // has a rank occurence greater than or equal to count
            return result.RankOccurrences.Where(kvp =>
            kvp.Key != Rank.None &&
            kvp.Value >= count).Any();
        }

        private static int SumPlayedChips(HandRankerResult result)
        {
            int sum = 0;
            foreach (var kvp in result.RankOccurrences)
            {
                for (int i = 0; i < kvp.Value; i++)
                {
                    sum += RankToChips[kvp.Key];
                }
            }
            return sum;
        }

        public static Dictionary<Rank, int> RankToChips = new()
    {
        { Rank.None, 0 },
        { Rank.Two, 2 },
        { Rank.Three, 3 },
        { Rank.Four, 4 },
        { Rank.Five, 5 },
        { Rank.Six, 6 },
        { Rank.Seven, 7 },
        { Rank.Eight, 8 },
        { Rank.Nine, 9 },
        { Rank.Ten, 10 },
        { Rank.Jack, 10 },
        { Rank.Queen, 10 },
        { Rank.King, 10 },
        { Rank.Ace, 11 },
        { Rank.Joker, 100 }
    };


        public static Dictionary<HandType, int> HandTypeToDamage = new()
    {
        { HandType.FlushFive, 512 },
        { HandType.FlushHouse, 392 },
        { HandType.FiveOfKind, 288 },
        { HandType.RoyalFlush, 160 },
        { HandType.StraightFlush, 160 },
        { HandType.FourOfKind, 84 },
        { HandType.FullHouse, 32 },
        { HandType.Flush, 28 },
        { HandType.Straight, 24 },
        { HandType.ThreeOfKind, 18 },
        { HandType.TwoPair, 8 },
        { HandType.Pair, 4 },
        { HandType.HighCard, 1 },
        { HandType.None, 0 }
    };
    }

    public enum HandType
    {
        None,
        HighCard,
        Pair,
        TwoPair,
        ThreeOfKind,
        Straight,
        Flush,
        FullHouse,
        FourOfKind,
        StraightFlush,
        RoyalFlush,
        FiveOfKind,
        FlushHouse,
        FlushFive
    }

}
