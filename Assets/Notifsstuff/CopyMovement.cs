using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMovementWithOffset : MonoBehaviour
{
    public Transform objectToCopy;  // Reference to the object to copy (e.g., Main Camera)
    private Vector3 positionOffset; // Initial position offset
    private Quaternion rotationOffset; // Initial rotation offset (if you want to maintain one)

    void Start()
    {
        // Calculate the initial offset between the cube and the objectToCopy (camera).
        positionOffset = transform.position - objectToCopy.position;
        
        // Optional: If you want to maintain a rotation offset as well, calculate it here.
        // rotationOffset = Quaternion.Inverse(objectToCopy.rotation) * transform.rotation;
    }

    void Update()
    {
        // Copy the position while keeping the initial offset.
        transform.position = objectToCopy.position + positionOffset;
        
        // Copy the rotation exactly (or modify this part if you want to maintain a rotational offset)
        transform.rotation = objectToCopy.rotation;
        // Optional: To apply a constant rotation offset, use:
        // transform.rotation = objectToCopy.rotation * rotationOffset;
    }
}
