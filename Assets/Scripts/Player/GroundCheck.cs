using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{   
    public Transform rayOrigin;
    public LayerMask groundLayers;
    public float detectionDistance;

    public bool IsTouchingGround()
    {
        return Physics.Raycast(rayOrigin.position, -rayOrigin.up, detectionDistance, groundLayers);
    }
}
