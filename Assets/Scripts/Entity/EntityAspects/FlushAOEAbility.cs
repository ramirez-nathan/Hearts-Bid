using UnityEngine;
using System.Collections;
using Scripts.Hand;
using Scripts.Card;
using System.Collections.Generic;

public class FlushAOEAbility : MonoBehaviour
{
    protected HandRankerResult hand;
    protected int damage = 0;
    SpriteRenderer sprite;
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>(); // Track hit enemies

    private void Awake()
    {
        
        
    }

    public void Initialize(HandRankerResult hand)
    {
        this.hand = hand;
        if (hand == null || hand.InspectedCards == null || hand.InspectedCards.Count == 0)
        {
            Debug.LogError("HandRankerResult is null or does not contain inspected cards.");
            return;
        }
        sprite = GetComponent<SpriteRenderer>();
        int baseDamage = HandRanker.HandTypeToDamage[hand.BestHand];
        int totalChips = hand.TotalPlayedChips;
        damage = (int)(baseDamage * totalChips * 0.4); 

        Color AOEcircle = Color.white; // default color
        switch (hand.InspectedCards[0].Suit)
        {
            case Suit.Hearts:
                AOEcircle = HexToColor("#FE9590");
                break;
            case Suit.Clubs:
                AOEcircle = HexToColor("#6ABE2F");
                break;
            case Suit.Spades:
                AOEcircle = HexToColor("#00D3FE");
                break;
            case Suit.Diamonds:
                AOEcircle = HexToColor("#FEF976");
                break;
        }

        AOEcircle.a = 0.18f; // Set alpha value to 90% (0.90)
        sprite.color = AOEcircle;
        StartCoroutine(AOEAttack());
    }
    private IEnumerator AOEAttack()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        return Color.white; // Return white if conversion fails
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!hitEnemies.Contains(collision.gameObject)) // Check if the enemy was already hit
            {
                Debug.Log("Enemy was hit by Flush AOE!");
                collision.gameObject.GetComponent<Entity>().TakeHit(damage);
                hitEnemies.Add(collision.gameObject); // Mark this enemy as hit
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!hitEnemies.Contains(collision.gameObject)) // Check if the enemy was already hit
            {
                Debug.Log("Enemy was hit by Flush AOE!");
                collision.gameObject.GetComponent<Entity>().TakeHit(damage);
                hitEnemies.Add(collision.gameObject); // Mark this enemy as hit
            }
        }
    }
}
