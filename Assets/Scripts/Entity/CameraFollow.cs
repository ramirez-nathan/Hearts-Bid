using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject playerToFollow;

    void LateUpdate()
    {
        transform.position = playerToFollow.transform.position + new Vector3(0, 0, -20); // need a z offset so camera isn't "in" player
    }
}
