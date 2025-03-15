using UnityEngine;
using UnityEngine.Tilemaps;

public class ChipProjectile : MonoBehaviour
{
    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    public void Fire(float force)
    {
        Destroy(this, 10f);

        rb2d.linearVelocity = transform.right * force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeHit(20);
            Destroy(this);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Tilemap"))
        {
            Destroy(this);
            gameObject.SetActive(false);
        }
    }
}
