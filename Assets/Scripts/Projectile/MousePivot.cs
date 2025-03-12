using UnityEngine;
using UnityEngine.PlayerLoop;

public class MousePivot : MonoBehaviour
{
    private void Update()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from object to mouse
        Vector2 direction = (mousePosition - transform.position).normalized;

        // Calculate angle and rotate the object
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
