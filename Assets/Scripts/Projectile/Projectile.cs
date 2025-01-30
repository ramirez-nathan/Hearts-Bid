using UnityEngine;
using UnityEngine.Rendering;

public class Projectile : MonoBehaviour
{
    private Transform target; // enemy
    private float moveSpeed;


    void Start()
    {
        target = GameObject.Find("Enemy").transform;
    }

    
    void Update()
    {
        
        
        
    }
    public void InitializeProjectile(Transform target, float moveSpeed)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        Vector3 moveDirNormalized = (target.position - transform.position).normalized;
        transform.position += moveDirNormalized * moveSpeed * Time.deltaTime;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null) // if its an enemy object
        {
            Destroy(gameObject); 
            Debug.Log("Destroyed Card");
        }
    }
}
