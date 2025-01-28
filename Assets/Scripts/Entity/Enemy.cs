using UnityEngine;

public class Enemy : Entity
{

    //copied from intial player script 
    private float moveSpeed = 2.0f;
    private float avoidRange = 2.0f;
    bool nearObstacle;
    float avoidStrength = 15.0f; //how hard to avoid obstacles



    Rigidbody2D enemyRb;

    Transform playerTarget;
    Transform obstacle;
    Vector2 moveDir;
    Vector2 avoidDir;

    

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

        nearObstacle = false;


        

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
                Vector2 blendedA = (moveDir + perpendicularDirectionA).normalized;
                Vector2 blendedB = (moveDir + perpendicularDirectionB).normalized;

                // Calculate distances to the player for each option
                float distanceA = Vector2.Distance(new Vector2(transform.position.x + blendedA.x,transform.position.y + blendedA.y), playerTarget.position);
                float distanceB = Vector2.Distance(new Vector2(transform.position.x + blendedB.x, transform.position.y + blendedB.y), playerTarget.position);

                // Choose the direction that minimizes the distance to the player
                avoidDir += (distanceA <= distanceB) ? perpendicularDirectionA : perpendicularDirectionB;
            }

            
            else if (!nearObstacle) //only reset in loop if you arent near something
            {
               avoidDir = Vector2.zero; //reset the direction for this
                
            }
            //TODO this cannot handle multiple obstacles in scene
            
        }

        
        // Combine player direction with avoidance direction
        if (avoidDir != Vector2.zero)
        {
            moveDir = (moveDir * (1f/avoidStrength) + avoidDir * avoidStrength).normalized;
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


    // Check if an obstacle is within the enemy's vision cone
    private float visionAngle = 35f;
    private float visionRange = 10f;
    bool IsObstacleInVision(GameObject obstacle)
    {
        Vector2 directionToObstacle = (obstacle.transform.position - transform.position).normalized;
        float angleToObstacle = Vector2.Angle(transform.up, directionToObstacle); // Assuming the forward direction is 'up' of the enemy

        if (angleToObstacle <= visionAngle / 2 && Vector2.Distance(transform.position, obstacle.transform.position) <= visionRange)
        {
            // Check for line of sight using raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToObstacle, visionRange);
            if (hit.collider != null && hit.collider.gameObject == obstacle)
            {
                return true; // Obstacle is within vision and line of sight
            }
        }
        return false; // Obstacle is outside the vision cone or blocked
    }

}




