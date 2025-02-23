using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HandRankerResult
{
    public List<Card> InspectedCards { get; private set; }  
    public HandType BestHand { get; set; } = HandType.None;
    public List<HandType> ApplicableHands { get; set; } = new();
    public Dictionary<Rank, int> RankOccurances { get; private set; }
    public Dictionary<Suit, int> SuitOccurances { get; private set; }

    public HandRankerResult(List<Card> cards)
    {
        RankOccurances = InitializeOccurances<Rank>();
        SuitOccurances = InitializeOccurances<Suit>();

        // tally how much of each rank & suit there are

        foreach (Card card in cards)
        {
            Tally(RankOccurances, card.Rank);
            Tally(SuitOccurances, card.Suit);
        }
    }

    //-- Helper Methods

    private static void Tally<T>(Dictionary<T, int> occurances, T target)
    {
        if (occurances.ContainsKey(target))
        {
            occurances[target]++;
        }
        else
        {
            Debug.LogError($"Unrecognized target \"{target} of type \"{target.GetType()}\".");
        }
    }

    private Dictionary<T, int> InitializeOccurances<T>()
    {
        Dictionary<T, int> occurances = new();

        foreach (T target in Enum.GetValues(typeof(T)))
        {
            occurances[target] = 0;
        }

        return occurances;
    }

}