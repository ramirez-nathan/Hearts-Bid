using UnityEngine;
using UnityEditor;
using System.IO;
using System;

namespace Scripts.Card
{
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

                    // Find sprite for card
                    string spriteName = $"{GetRankShortName(rank)}-{GetSuitShortName(suit)}";
                    string spritePath = $"Assets/Art/cards/cards/light/{spriteName}.png";

                    Sprite cardSprite = AssetDatabase.LoadAssetAtPath<Sprite>(spritePath);
                    if (cardSprite != null)
                    {
                        newCard.Sprite = cardSprite;
                    }
                    else
                    {
                        Debug.LogWarning($"Sprite not found: {spritePath}");
                    }

                    // Set a unique name for the asset
                    string assetName = $"{rank}_of_{suit}.asset";
                    string assetPath = $"Assets/Resources/Cards/{assetName}";

                    AssetDatabase.CreateAsset(newCard, assetPath);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        // Helper method to get sprite name for Rank
        private static string GetRankShortName(Rank rank)
        {
            switch (rank)
            {
                case Rank.Ace: return "A";
                case Rank.Jack: return "J";
                case Rank.Queen: return "Q";
                case Rank.King: return "K";
                case Rank.Joker: return "Joker";
                default: return ((int)rank + 1).ToString(); // For numbered ranks (2-10)
            }
        }

        // Helper method to get sprite name for Suit
        private static string GetSuitShortName(Suit suit)
        {
            switch (suit)
            {
                case Suit.Hearts: return "H";
                case Suit.Diamonds: return "D";
                case Suit.Spades: return "P";
                case Suit.Clubs: return "C";
                default: return string.Empty; // Shouldn't happen
            }
        }
    }
}
