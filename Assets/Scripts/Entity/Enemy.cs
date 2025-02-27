using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected float moveSpeed = 3.0f;
    [SerializeField] protected float avoidRange = 3.0f;
    [SerializeField] protected float spacingRange = 5.0f;
    [SerializeField] protected float avoidStrength = 1000.0f;

    protected bool nearObstacle;
    protected Transform playerTarget;
    protected Vector2 moveDir;
    protected Vector2 avoidDir;

    GameObject[] obstacles;
    GameObject[] enemies;

   
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        moveDir = direction;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        entityRb.rotation = angle;

        
        avoidDir = Vector2.zero;

        HandleObstacleAvoidance(direction);
        HandleEnemyAvoidance(direction);

        

        if (avoidDir != Vector2.zero)
        {
            //add avoid direction to movement, varying strength
            moveDir = (moveDir  + (avoidDir * avoidStrength)).normalized;
            //moveDir = avoidDir.normalized;
        }
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = (playerTarget.position - transform.position).magnitude;
        entityRb.linearVelocity = (playerTarget && distanceToPlayer > 2) ? moveDir * moveSpeed : Vector2.zero;
    }


    void HandleObstacleAvoidance(Vector2 direction)
    {


        foreach (GameObject obstacle in obstacles)
        {

            float distanceToObstacle = Vector2.Distance(transform.position, obstacle.transform.position);
          
            //for each obstacle, check if enemy is near it 
            if (distanceToObstacle <= avoidRange && IsObstacleInVision(obstacle, direction))
            {
            
                //direction of obstacles relative to enemy 
                Vector2 directionToObstacle = (obstacle.transform.position - transform.position).normalized;

                //take orthogonal vectors, left and right relative to direction to objevt 
                Vector2 perpendicularDirectionA = new Vector2(-directionToObstacle.y, directionToObstacle.x);
                Vector2 perpendicularDirectionB = new Vector2(directionToObstacle.y, -directionToObstacle.x);

                //blend the vectors with current movement vector 
                Vector2 blendedA = (moveDir + perpendicularDirectionA).normalized;
                Vector2 blendedB = (moveDir + perpendicularDirectionB).normalized;

                //take distance from both orthogonal vectors to player
                float distanceA = Vector2.Distance(transform.position + (Vector3)blendedA, playerTarget.position);
                float distanceB = Vector2.Distance(transform.position + (Vector3)blendedB, playerTarget.position);

                //avoid direction goes towards orthogonal vector that results in closest distance
                avoidDir += ((distanceA <= distanceB) ? perpendicularDirectionA : perpendicularDirectionB);
            }
        }
    }

    void HandleEnemyAvoidance(Vector2 direction)
    {


        foreach (GameObject obstacle in enemies)
        {

            float distanceToObstacle = Vector2.Distance(transform.position, obstacle.transform.position);

            //for each obstacle, check if enemy is near it 
            if (distanceToObstacle <= spacingRange && IsObstacleInVision(obstacle, direction))
            {

                //direction of obstacles relative to enemy 
                Vector2 directionToObstacle = (obstacle.transform.position - transform.position).normalized;

                //take orthogonal vectors, left and right relative to direction to objevt 
                Vector2 perpendicularDirectionA = new Vector2(-directionToObstacle.y, directionToObstacle.x);
                Vector2 perpendicularDirectionB = new Vector2(directionToObstacle.y, -directionToObstacle.x);

                //blend the vectors with current movement vector 
                Vector2 blendedA = (moveDir + perpendicularDirectionA).normalized;
                Vector2 blendedB = (moveDir + perpendicularDirectionB).normalized;

                //take distance from both orthogonal vectors to player
                float distanceA = Vector2.Distance(transform.position + (Vector3)blendedA, playerTarget.position);
                float distanceB = Vector2.Distance(transform.position + (Vector3)blendedB, playerTarget.position);

                //avoid direction goes towards orthogonal vector that results in closest distance
                avoidDir += ((distanceA <= distanceB) ? perpendicularDirectionA : perpendicularDirectionB);
            }
        }
    }


    bool IsObstacleInVision(GameObject obstacle, Vector2 direction)
    {
       
        Vector2 directionToObstacle = (obstacle.transform.position - transform.position).normalized;
        float angleToObstacle = Vector2.Angle(direction, directionToObstacle);

        //if the obstacle is in front view of enemy
        return angleToObstacle <= 90f;
    }

}



