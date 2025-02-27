using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] spawnOptions; // Array of prefabs to spawn
    [SerializeField] float sizeX = 1f;
    [SerializeField] float sizeY = 1f;
    [SerializeField] float spawnCooldown = 5f;
    [SerializeField] int round = 0;

    private float spawnTime;

    void Start()
    {
        spawnTime = spawnCooldown;
    }

    void Update()
    {
        if (spawnTime > 0)
            spawnTime -= Time.deltaTime;

        if (spawnTime <= 0)
        {
            Spawn();
            spawnTime = spawnCooldown;
        }
    }

    void Spawn()
    {
        if (spawnOptions.Length == 0) return; // Safety check

        float xPos = (Random.value - 0.5f) * 2 * sizeX + gameObject.transform.position.x;
        float yPos = (Random.value - 0.5f) * 2 * sizeY + gameObject.transform.position.y;

        // Pick a random prefab from the list
        GameObject selectedPrefab = spawnOptions[Random.Range(0, spawnOptions.Length)];
        var spawn = Instantiate(selectedPrefab);
        spawn.transform.position = new Vector3(xPos, yPos, 0);
    }
}
