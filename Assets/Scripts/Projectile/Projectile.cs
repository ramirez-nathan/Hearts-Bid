using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target; // enemy
    private float moveSpeed;

    private float distToTargetToDestroyProjectile = 1f;

    void Start()
    {
        
    }

    
    void Update()
    {
        
        
        
    }
    void FixedUpdate()
    {
        Vector3 moveDirNormalized = (target.position - transform.position).normalized;
        transform.position += moveDirNormalized * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, moveDirNormalized) < distToTargetToDestroyProjectile)
        {
            Destroy(gameObject);
            Debug.Log("Destroyed Card");
        }
    }

    public void InitializeProjectile(Transform target, float moveSpeed)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
    }
}
