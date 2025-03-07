
using UnityEngine;
using UnityEngine.UI;





public class Score : MonoBehaviour
{

    [SerializeField] GameObject spawnerPrefab;
    [SerializeField] Text scoreText;
    EnemySpawner enemySpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemySpawner = spawnerPrefab.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = enemySpawner.getRound().ToString();
    }
}
