using System.Runtime.Serialization;
using UnityEngine;

public class Enemy : Entity
{
    
    //copied from intial player script 
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float avoidRange = 2.0f;
    [SerializeField] bool nearObstacle;
    [SerializeField] float avoidStrength = 15.0f; //how hard to avoid obstacless
    
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
            

            if (distanceToObstacle <= avoidRange && IsObstacleInVision(obstacle, direction))
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


   

    private float visionRange = 3.0f; // Vision range in units

    bool IsObstacleInVision(GameObject obstacle, Vector2 direction)
    {
        // Direction from the enemy to the obstacle
        Vector2 directionToObstacle = (obstacle.transform.position - transform.position).normalized;

        // Calculate the angle between the enemy's facing direction and the direction to the obstacle
        float angleToObstacle = Vector2.Angle(direction, directionToObstacle);

        // Check if the obstacle is not behind the enemy (angle <= 90 degrees)
        if (angleToObstacle <= 90f && Vector2.Distance(transform.position, obstacle.transform.position) <= visionRange)
        {
            return true; // The obstacle is not behind and is within the vision range
        }

        return false; // The obstacle is behind or out of range
    }

}




