using Scripts.Card;
using Scripts.Deck;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalAbilitySystem
{
    // For each ability, store the next time it can be used.
    private static Dictionary<GlobalAbilityType, float> nextUseTime = new();

    // The base cooldown for each ability type
    private static Dictionary<GlobalAbilityType, float> cooldowns = new()
    {
        { GlobalAbilityType.CallAllHands, 4f },
    };

    // A global event that broadcasts which ability was triggered
    public static event Action<GlobalAbilityType> GlobalAbilityBehavior;

    public static void TriggerAbility(GlobalAbilityType ability)
    {
        float currentTime = Time.time;

        // If we've never used this ability before, ensure a default of 0
        if (!nextUseTime.ContainsKey(ability))
            nextUseTime[ability] = 0f;

        // Check if it's still on cooldown
        if (currentTime < nextUseTime[ability])
        {
            Debug.Log($"{ability} is on cooldown!");
            return;
        }

        // We can use the ability now—set the next time it can be used
        float abilityCooldown = cooldowns.ContainsKey(ability) ? cooldowns[ability] : 4f;
        nextUseTime[ability] = currentTime + abilityCooldown;

        // Fire the event
        GlobalAbilityBehavior?.Invoke(ability);
    }
}


