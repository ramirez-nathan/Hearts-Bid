using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public bool isOnCooldown = false;  // To track if ability is on cooldown

    // Abstract property to get the cooldown time for each specific ability
    protected abstract float Cooldown { get; }

    // Called by the player to try activating the ability
    public void TryActivate()
    {
        if (!isOnCooldown)  // Check if ability is not on cooldown
        {
            Activate();
            StartCoroutine(StartCooldown());  // Start the cooldown coroutine
        }
        else
        {
            Debug.Log($"{GetType().Name} is on cooldown!");
        }
    }

    // Abstract method to be implemented by derived classes (i.e., actual ability logic)
    protected abstract void Activate();

    // Coroutine to manage the cooldown period
    private IEnumerator StartCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(Cooldown);  // Use the concrete ability's cooldown
        isOnCooldown = false;
    }
}
