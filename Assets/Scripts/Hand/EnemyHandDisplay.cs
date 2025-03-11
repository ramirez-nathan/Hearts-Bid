using Scripts.Hand;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyHandDisplay : HandDisplay
{
    [SerializeField] TMP_Text handText;
    [SerializeField] Image enemyImage;
    [SerializeField] EnemyTrackingAbility enemyTracker;

    private void Awake()
    {
        if (enemyTracker == null)
        {
            Debug.LogError("Enemy Tracker is null in hand display, cannot display");
        }
    }

    private void Update()
    {
        EnemyHand enemyHand = enemyTracker?.closestEnemy.GetComponent<EnemyHand>();

        if (enemyHand == null) DisplayEnemyHand(enemyHand);
    }

    public void DisplayEnemyHand(EnemyHand enemyHand)
    {
        // show the cards
        DisplayHand(enemyHand);

        // show the best hand possible as string
        var result = HandRanker.RankHand(enemyHand);
        handText.text = $"Hand: {HandRanker.HandTypeToString[result.BestHand]}";

        // show enemy's sprite
        enemyImage.sprite = enemyHand.GetComponent<SpriteRenderer>().sprite;
    }
}
