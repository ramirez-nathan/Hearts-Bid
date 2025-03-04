using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemy : Entity
{

    protected NavMeshAgent agent;
    protected Transform playerTarget;
    int maxHealth = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTarget.position);
       
    }

}
