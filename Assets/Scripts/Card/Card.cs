using UnityEngine;

[CreateAssetMenu(fileName = "newCard", menuName = "New Card")]
public class Card : ScriptableObject
{
    public Suit Suit = Suit.None;
    public Rank Rank = Rank.None;
    public Sprite Sprite = null;
}

public enum Suit
{
    None,
    Hearts,
    Diamonds,
    Spades,
    Clubs
}

public enum Rank
{
    None,
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
    Joker = 100
}