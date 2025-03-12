using UnityEngine;

public class PlayerPivot : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the direction from this object to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Calculate the angle and rotate the object
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
