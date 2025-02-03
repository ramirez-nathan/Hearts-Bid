using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Transform target; // enemy
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector3 projectileMoveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
        
        
    }
    public Vector3 GetProjectileMoveDirection()
    {
        return projectileMoveDirection;
    }

    public void InitializeProjectile(Transform target, float moveSpeed)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
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
            Debug.Log("Collided with layer: " + collision.gameObject.layer);
            Debug.Log("Destroyed Card");
            Destroy(this.gameObject); // destroy card
        }
    }

    
}
