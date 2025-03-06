using UnityEngine;
using System.Collections;

public class DodgeAbility : Ability
{
    private Player player;  // Reference to the Player
    public float speedMultiplier = 15.0f;
    public float duration = 10f;

    // Override the abstract Cooldown property
    [SerializeField] protected override float Cooldown => 1.5f; // Set the cooldown for the dodge ability

    private void Awake()
    {
        player = GetComponent<Player>();  // Check if this is correctly finding the player component
        if (player == null)
        {
            Debug.LogError("Player component not found on the GameObject.");
        }
    }

    // Set the player reference during initialization
    public void Initialize(Player player)
    {
        this.player = player;
    }

    // Implement the abstract Activate method
    protected override void Activate()
    {
        // Perform the dodge logic (e.g., boost player speed)
        player.moveSpeed *= speedMultiplier;

        // Set a timer to reset the speed after 'duration' seconds
        player.StartCoroutine(ResetSpeedAfterDuration());
    }

    private IEnumerator ResetSpeedAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        player.moveSpeed /= speedMultiplier;  // Reset player speed after the dodge duration
        Debug.Log("Dodge ended and speed reset.");
    }
}
