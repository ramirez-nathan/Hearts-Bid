using UnityEngine;

public class Enemy : Entity
{

    //copied from intial player script 
    private float moveSpeed = 2.0f;
    private float avoidRange = 2.5f;
    


    Rigidbody2D enemyRb;

    Transform playerTarget;
    Transform obstacle;
    Vector2 moveDir;
    Vector2 avoidDir;
    bool nearObstacle;
    

    GameObject[] obstacles; // Array to hold all obstacle objects

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;

         
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
    }

    // Update is called once per frame
    void Update()
    { 
        
        //tutorial had vector3 not sure why, change if creates issues 
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        moveDir = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyRb.rotation = angle;

        //new part to be reviewed
        foreach (GameObject obstacle in obstacles)
        {

            float distanceToObstacle = Vector2.Distance(transform.position, obstacle.transform.position);
            if (distanceToObstacle <= avoidRange)
            {
                nearObstacle = true;
                // Avoid this obstacle by adding a direction away from it
                Vector2 directionToObstacle = (obstacle.transform.position - transform.position).normalized;

                // Add a perpendicular avoidance direction
                Vector2 perpendicularDirectionA = new Vector2(-directionToObstacle.y, directionToObstacle.x);
                Vector2 perpendicularDirectionB = new Vector2(directionToObstacle.y, -directionToObstacle.x);

                //create new directions to assess shortest 
                Vector3 blendedA = (moveDir + perpendicularDirectionA).normalized;
                Vector3 blendedB = (moveDir + perpendicularDirectionB).normalized;

                // Calculate distances to the player for each option
                float distanceA = Vector3.Distance(transform.position + blendedA, playerTarget.position);
                float distanceB = Vector3.Distance(transform.position + blendedB, playerTarget.position);

                // Choose the direction that minimizes the distance to the player
                avoidDir += (distanceA < distanceB) ? perpendicularDirectionA : perpendicularDirectionB;
            }

            
            else
            {
               avoidDir = Vector2.zero; //reset the direction for this
                
            }
            //TODO this cannot handle multiple obstacles in scene

        }
        

        // Combine player direction with avoidance direction
        if (avoidDir != Vector2.zero)
        {
            moveDir = (moveDir + avoidDir).normalized;
        }

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
