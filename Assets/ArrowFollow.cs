using UnityEngine;

public class ArrowFollow : MonoBehaviour
{
    public Camera mainCamera;
    public Vector3 offset = new Vector3(-0.6f, -1.4f, 0.8f); // Adjust this based on your arrow's initial position

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Calculate the desired position in the bottom left corner relative to the camera
        Vector3 bottomLeftCorner = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane + offset.z));
        transform.position = bottomLeftCorner + new Vector3(offset.x, offset.y, 0);

        // Always face the arrow towards the camera's forward direction
        transform.rotation = Quaternion.LookRotation(mainCamera.transform.forward);
    }
}
