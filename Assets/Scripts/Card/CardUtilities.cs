using Scripts.Card;
using UnityEngine;

public static class CardUtilities
{
    public static Card MakeTempCard(Rank rank, Suit suit)
    {
        return new Card
        {
            Rank = rank,
            Suit = suit,
        };
    }

    public static Card MakeRandomTempCard()
    {
        var randomRank = GetRandomEnum<Rank>();
        var randomSuit = GetRandomEnum<Suit>();

        return MakeTempCard(randomRank, randomSuit);
    }

    public static T GetRandomEnumInRange<T>(int lowInclusive, int maxExclusive) where T : System.Enum
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T));
        return values[Random.Range(lowInclusive, maxExclusive)];
    }

    public static T GetRandomEnum<T>() where T : System.Enum
    {
        T[] values = (T[])System.Enum.GetValues(typeof(T));
        return values[Random.Range(0, values.Length)];
    }
}
