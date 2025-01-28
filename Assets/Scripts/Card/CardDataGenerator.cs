using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CardDataGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Cards")]
    public static void GenerateCards()
    {
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                // Skip blank cards
                if (suit == Suit.None || rank == Rank.None)
                {
                    continue;
                }

                // Create card
                Card newCard = ScriptableObject.CreateInstance<Card>();
                newCard.Suit = suit;
                newCard.Rank = rank;

                // Set a unique name for the asset
                string assetName = $"{rank}_of_{suit}.asset";
                string assetPath = $"Assets/Cards/{assetName}";

                AssetDatabase.CreateAsset(newCard, assetPath);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}