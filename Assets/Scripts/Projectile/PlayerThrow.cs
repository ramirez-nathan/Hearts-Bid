using System;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField] GameObject throwAnimationPrefab;

    internal void PlayThrowAnimation()
    {
        Instantiate(throwAnimationPrefab, this.transform);
    }
}
