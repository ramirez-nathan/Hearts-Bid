using UnityEngine;
using UnityEngine.AI;

public class NavMeshEnemy : Entity
{

    protected NavMeshAgent agent;
    protected Transform playerTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTarget = GameObject.Find("Player").transform;
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerTarget.position);
       
    }
}
