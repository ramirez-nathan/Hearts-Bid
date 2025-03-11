using Scripts.Card;
using Scripts.Deck;
using Scripts.Hand;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;
    public Card cardData; // Store card information
    private Deck returnDeck;
    private Player returnPlayer;

    Action currentBehavoir = null;

    private Rigidbody2D rb;
    private Vector3 projectileMoveDirection;
    public SpriteRenderer spriteRenderer;
    private bool isCachedOnEnemy = false;
    [SerializeField] private bool returningToPlayer = false;

    private void Awake()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector3 GetProjectileMoveDirection()
    {
        return projectileMoveDirection;
    }

    public void InitializeProjectile(Transform target, float moveSpeed, Card card, Deck returnDeck, Player returnPlayer)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.cardData = card;
        this.returnDeck = returnDeck;
        this.returnPlayer = returnPlayer;

        if (cardData == null)
        {
            Debug.LogError("cardData is NULL! Make sure a valid Card object is passed.");
            return;
        }
        if (cardData != null && cardData.Sprite != null)
        {
            spriteRenderer.sprite = cardData.Sprite;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer was missing! Adding one.");
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = cardData.Sprite; 
        //Debug.Log($"Projectile launched with {cardData.name}");

        FindFirstObjectByType<AudioManager>().Play("Throw"); // play throw sound effect

        currentBehavoir = MoveToEnemy;
    }

    void FixedUpdate()
    {
        currentBehavoir?.Invoke(); // if currbehavior exists, update
    }

    public void BeginReturnToPlayer(float delay)
    {
        StartCoroutine(ReturnToPlayer(delay));
    }

    IEnumerator ReturnToPlayer(float delay)
    {
        target = returnPlayer.transform; // tracks player
        spriteRenderer.enabled = true; // re-draws card
        returningToPlayer = true; // to track current behavior for collisions
        yield return new WaitForSeconds(delay);
        currentBehavoir = ReturnToPlayer;
    }

    public void MoveToEnemy()
    {
        MoveToTarget(moveSpeed);
    }

    private void MoveToTarget(float speed)
    {
        if (target != null) 
        {
            Vector3 moveDirNormalized = (target.position - transform.position).normalized;
            projectileMoveDirection = moveDirNormalized * speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + (Vector2)projectileMoveDirection);
        }
        else
        {
            if (returnPlayer.transform)
            {
                target = returnPlayer.transform;
                returningToPlayer = true;
                currentBehavoir = ReturnToPlayer;
            }
            else
            {
                ReturnCachedCards();
                Destroy(this.gameObject);
            }
        }
    }

    public void ReturnToPlayer()
    {
        MoveToTarget(moveSpeed * 3f);
    }

    public void ReturnCachedCards()
    { 
        returnDeck.ReturnToDeck(cardData);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") &&
            collision.gameObject == target.gameObject) // if its an enemy object
        {
            if (isCachedOnEnemy) return;
            int chipDmg = HandRanker.RankToChips[cardData.Rank];
            collision.gameObject.GetComponent<Entity>().TakeHit(chipDmg); // chip dmg for value of card
            collision.gameObject.GetComponent<EnemyHand>().AddCardToCache(this); // cache card to enemy hand


            FindFirstObjectByType<AudioManager>().Play("Enemy Damaged");
            isCachedOnEnemy = true;
         
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player") &&
                 returningToPlayer)
        {
            isCachedOnEnemy = false;
            ReturnCachedCards();
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && returningToPlayer)
        {
            ReturnCachedCards();
            Destroy(this.gameObject);
        }
    }
}
