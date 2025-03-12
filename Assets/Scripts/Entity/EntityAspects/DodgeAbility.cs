using UnityEngine;
using System.Collections;

public class DodgeAbility : Ability
{
    private Player player;  // Reference to the Player
    public float speedMultiplier;
    public bool isDodging = false;
    public float duration;

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

        FindFirstObjectByType<AudioManager>().Play("Dodge"); // play dodge sound effect
    }
    
    private IEnumerator ResetSpeedAfterDuration()
    {
        isDodging = true;
        
        yield return new WaitForSeconds(duration);
        isDodging = false;
        player.moveSpeed /= speedMultiplier;  // Reset player speed after the dodge duration
        Debug.Log("Dodge ended and speed reset.");
    }
}
