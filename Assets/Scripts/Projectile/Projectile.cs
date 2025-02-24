using Scripts.Card;
using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;
    private Card cardData; // Store card information

    private Rigidbody2D rb;
    private Vector3 projectileMoveDirection;
    private SpriteRenderer spriteRenderer; 

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
        Vector3 moveDirNormalized = (target.position - transform.position).normalized;
        projectileMoveDirection = moveDirNormalized * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + (Vector2)projectileMoveDirection);

    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy")) // if its an enemy object
        {
            Debug.Log("Destroyed Card");
            Destroy(this.gameObject); // destroy card

            FindObjectOfType<AudioManager>().Play("Enemy Damaged"); // play enemy damaged sound effect
        }
    }

    
}
