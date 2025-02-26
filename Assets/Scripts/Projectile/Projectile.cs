using Scripts.Card;
using Scripts.Hand;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;
    public Card cardData; // Store card information

    private Rigidbody2D rb;
    private Vector3 projectileMoveDirection;
    public SpriteRenderer spriteRenderer;
    private bool isCachedOnEnemy = false;
    private bool returningToPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

   
    public Vector3 GetProjectileMoveDirection()
    {
        return projectileMoveDirection;
    }

    public void InitializeProjectile(Transform target, float moveSpeed, Card card)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
        this.cardData = card;
        if (cardData == null)
        {
            Debug.LogError("cardData is NULL! Make sure a valid Card object is passed.");
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer was missing! Adding one.");
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = cardData.Sprite; 
        Debug.Log($"Projectile launched with {cardData.name}");

        FindFirstObjectByType<AudioManager>().Play("Throw"); // play throw sound effect
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 moveDirNormalized = (target.position - transform.position).normalized;
            projectileMoveDirection = moveDirNormalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + (Vector2)projectileMoveDirection);
        }
        else
        {
            target = FindFirstObjectByType<Player>().transform;
            spriteRenderer.enabled = true;
            returningToPlayer = true;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCachedOnEnemy) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") &&
            collision.gameObject == target.gameObject) // if its an enemy object
        {
            Debug.Log("Destroyed Card");
            collision.gameObject.GetComponent<EnemyHand>().AddCardToCache(this);


            FindFirstObjectByType<AudioManager>().Play("Enemy Damaged");
            isCachedOnEnemy = true;
            //Destroy(this.gameObject); // destroy card
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player") &&
                 returningToPlayer)
        {
            Destroy(this.gameObject);
        }
    }
}
