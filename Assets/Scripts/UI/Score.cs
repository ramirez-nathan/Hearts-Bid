
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class Score : MonoBehaviour
{

    [SerializeField] GameObject spawnerPrefab;
    [SerializeField] TMP_Text scoreText;
    EnemySpawner enemySpawner;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemySpawner = spawnerPrefab.GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Round: " + enemySpawner.getRound().ToString();
    }
}
