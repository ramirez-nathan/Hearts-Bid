using Scripts.Card;
using Scripts.Deck;
using UnityEngine;

public class ThrowCardAbility : Ability
{
    [SerializeField] private GameObject projectilePrefab;  // Reference to the projectile prefab
    [SerializeField] public Transform target;  // The target the card is thrown at
    [SerializeField] private float projectileMoveSpeed;  // Speed of the thrown card

    private Player returnPlayer;
    public Card cardToThrow; //which card to throw from the deck 

    // Implement the cooldown time specific to the ThrowCard ability
    protected override float Cooldown => 0.1f;  // 1 second cooldown, for example


    // The actual ability logic, similar to your ThrowCard method
    protected override void Activate()
    {
        Vector3 moveDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;

        // Apply a 90-degree rotation
        Quaternion spawnRotation = Quaternion.Euler(0, 0, angle + 90);

        // Instantiate the projectile and initialize it
        Projectile projectile = Instantiate(projectilePrefab, transform.position, spawnRotation).GetComponent<Projectile>();
        projectile.InitializeProjectile(target, projectileMoveSpeed, cardToThrow, FindFirstObjectByType<Deck>(), returnPlayer);
    }

    public void SetPlayer(Player player)
    {
        returnPlayer = player;
    }
}


