using UnityEngine;

public class DestructibleAnimation : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
