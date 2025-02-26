using Scripts.Hand;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Attributes
    protected int health;            // Representing the health attribute.
    
    protected Rigidbody2D entityRb;  // Shared rigidbody across entities


    private void Awake()
    {
        entityRb = GetComponent<Rigidbody2D>();
    }

    // Method to take damage
    public void TakeHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Virtual method for attack, can be overridden
    public virtual void Attack()
    {
        // Logic for attack (to be overridden in derived classes)
    }

    // Virtual method for death, can be overridden
    public virtual void Die()
    {
        // Logic for death
        Debug.Log("Entity died.");
        Destroy(this.gameObject);
    }
}