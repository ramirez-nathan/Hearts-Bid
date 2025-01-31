using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    [SerializeField] private Transform projectileVisual;
    [SerializeField] private Projectile projectile;
    
    

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateProjectileRotation();
    }

    private void UpdateProjectileRotation()
    {
        if (projectile == null || projectileVisual == null) return; 

        Vector3 projectileMoveDir = projectile.GetProjectileMoveDirection();

        if (projectileMoveDir.sqrMagnitude > 0.001f) // Only update if moving (avoids flickering)
        {
            projectileVisual.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectileMoveDir.y, projectileMoveDir.x) * Mathf.Rad2Deg + 90);
        }
        Debug.Log("ProjectileMoveDirection: " + projectileMoveDir);
    }
}
