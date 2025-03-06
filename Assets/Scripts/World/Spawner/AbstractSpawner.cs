using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public abstract class AbstractSpawner : MonoBehaviour
{
    protected abstract float spawnRate { get; }
    protected abstract GameObject[] SpawnTypes { get; }
    protected abstract int maxSpawn { get; }

    protected int numSpawned = 0;

    bool isOnCooldown = false; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //not sure we need anything here 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!isOnCooldown && (numSpawned < maxSpawn))
        {
            numSpawned++;
            Spawn();
            StartCoroutine(WaitToSpawn());
        }
        

    }

    private IEnumerator WaitToSpawn()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(spawnRate);  // Use the concrete spawners spawnRate
        isOnCooldown = false;
    }
    
    protected abstract void Spawn();
}
