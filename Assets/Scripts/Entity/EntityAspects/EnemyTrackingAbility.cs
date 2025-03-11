using Scripts.Hand;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//public class EnemyTrackingAbility : Ability

//could change this to just EnemyTracking since it no longer derives the ability abstract class
public class EnemyTrackingAbility : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public float detectionRadius = 5f;

    public Transform closestEnemy = null;
    private bool lockedOn = false;
    private bool started = false;


    public void Activate()
    {


        if (!lockedOn || (lockedOn && closestEnemy == null))
        {
            if (lockedOn)
            {
                lockedOn = false;
            }

            // Convert mouse position to world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f; // Set the z to 0 to avoid affecting the comparison

            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
            Transform newClosestEnemy = null;
            float minDistance = Mathf.Infinity; //this can be edited 

            if (enemies.Length > 0)
            {
                foreach (GameObject enemy in enemies)
                {
                    float distance = Vector3.Distance(mousePosition, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        newClosestEnemy = enemy.transform;
                    }
                }

                if (newClosestEnemy != closestEnemy && newClosestEnemy != null)
                {
                    closestEnemy = newClosestEnemy;
                    //closestEnemy.gameObject.GetComponent<EnemyHand>().LogHandAndRank();
                }
            }
        }
    }


    public void switchLock()
    {
        if (!lockedOn) { lockedOn = true; }
        else { lockedOn = false; }

    }






}
