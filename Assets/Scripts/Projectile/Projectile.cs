using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

    private float distToTargetToDestroyProjectile = 1f;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 moveDirNormalized = (target.position - transform.position).normalized;
        transform.position += moveDirNormalized * moveSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, moveDirNormalized) < distToTargetToDestroyProjectile)
        {
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {

    }

    public void InitializeProjectile(Transform target, float moveSpeed)
    {
        this.target = target;
        this.moveSpeed = moveSpeed;
    }
}
