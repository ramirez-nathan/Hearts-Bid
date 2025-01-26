using UnityEngine;

public class Enemy : Entity
{

    //copied from intial player script 
    private float moveSpeed = 2.0f;

    Rigidbody2D enemyRb;

    Transform playerTarget;
    Vector2 moveDir;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    { 
        
        //tutorial had vector3 not sure why, change if creates issues 
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        moveDir = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRb.rotation = angle;
        
    }

    private void FixedUpdate()
    {
        //logic for handling when close to a player, if close stand still 
        float distanceToPlayer = (playerTarget.position - transform.position).magnitude;
        if (playerTarget && distanceToPlayer > 2)
        {
            enemyRb.linearVelocity = moveDir * moveSpeed;
        }

        else
        {
            enemyRb.linearVelocity = Vector2.zero;
        }
    }
}
