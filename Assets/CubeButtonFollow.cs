using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeButtonFollow : MonoBehaviour
{
    public Transform cameraTransform; // Assign the Main Camera here
    public Vector3 offset = new Vector3(0.5f, -0.3f, 1.0f); // Adjust the offset as needed

    private void LateUpdate()
    {
        // Update the position of the cube relative to the camera's position and rotation
        transform.position = cameraTransform.position + cameraTransform.TransformDirection(offset);
        transform.rotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
    }
}
