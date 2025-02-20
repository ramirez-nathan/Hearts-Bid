using UnityEngine;



public class SpreadingNavMeshEnemy : NavMeshEnemy
{
    public float avoidanceRadius = 5f; // Radius to check for nearby enemies
    public float spreadForce = 10f; // Force to push enemies away

    private void Update()
    {
        AvoidNearbyEnemies();
        agent.SetDestination(playerTarget.position); // Set the destination after avoidance
    }

    private void AvoidNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, avoidanceRadius);
        Vector3 avoidanceForce = Vector3.zero;

        foreach (var collider in hitColliders)
        {
            if (collider != GetComponent<Collider>()) // Ignore itself
            {
                Vector3 direction = transform.position - collider.transform.position;
                avoidanceForce += direction.normalized / direction.magnitude; // Spread out
            }
        }

        // Apply force to move away
        if (avoidanceForce != Vector3.zero)
        {
            avoidanceForce.Normalize();
            transform.position += avoidanceForce * spreadForce * Time.deltaTime; // Move the enemy away
        }
    }
}

