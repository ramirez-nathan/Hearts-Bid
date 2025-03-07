using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractSpawner : MonoBehaviour
{
    protected abstract float spawnRate { get; }
    protected abstract string spawnerTag { get; }

    protected abstract string spawneeTag { get; } 

    protected abstract GameObject[] spawnTypes { get; }



    //tracks spawnees that are still alive 
    protected List<GameObject> spawnsAlive;


    protected int maxSpawn = 1;

    protected int numSpawned = 0;

    bool isOnCooldown = false;

    private int round = 0;

    protected Transform[] spawnAreas;




    void Start()
    {
        // Get all game objects with the "Spawner" tag
        GameObject[] spawners = GameObject.FindGameObjectsWithTag(spawnerTag);


        // Create a list of spawn points based on their Transforms
        spawnAreas = new Transform[spawners.Length];

        // Populate the spawnAreas array with the Transforms of each spawner
        for (int i = 0; i < spawners.Length; i++)
        {
            spawnAreas[i] = spawners[i].transform;
        }

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

        spawnsAlive = new List<GameObject>(GameObject.FindGameObjectsWithTag(spawneeTag));


        if (spawnsAlive.Count == 0)
        {
            round++;
            numSpawned = 0;
            maxSpawn += round * 5;
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
