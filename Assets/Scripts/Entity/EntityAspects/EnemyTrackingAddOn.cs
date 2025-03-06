using UnityEngine;
using UnityEngine.UI;

public class EnemyTrackingAddOn
{
    public string enemyTag = "Enemy";
    private Transform closestEnemy = null;
    private Outline lastOutline = null;

    public void UpdateTracking()
    {
        FindClosestEnemyToMouse();
    }

    private void FindClosestEnemyToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform newClosestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(mousePosition, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                newClosestEnemy = enemy.transform;
            }
        }

        if (newClosestEnemy != closestEnemy) // call enemy function loghandandrank()
        {
            Debug.Log("New enemy highlighted");
            // Remove outline from the previous closest enemy
            if (lastOutline != null)
            {
                lastOutline.enabled = false;
            }

            // Apply outline to the new closest enemy
            if (newClosestEnemy != null)
            {
                Outline outline = newClosestEnemy.GetComponent<Outline>();

                // If the enemy doesn't already have an Outline component, add one
                if (outline == null)
                {
                    outline = newClosestEnemy.gameObject.AddComponent<Outline>();
                }

                outline.enabled = true;
                lastOutline = outline;
            }

            closestEnemy = newClosestEnemy;
        }
    }

    public Transform GetClosestEnemy()
    {
        return closestEnemy;
    }
}
