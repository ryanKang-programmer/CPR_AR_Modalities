using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButton : MonoBehaviour
{
    private Renderer cubeRenderer;
    private Color originalColor;
    private float startTime;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        originalColor = cubeRenderer.material.color; // Store the original color of the cube
        startTime = Time.time; //start time
    }

    // This function will be called when the cube is clicked
    public void OnCubeClicked()
    {

        // Record the time of the button press
        float timeSinceStart = Time.time - startTime;
        Debug.Log($"{gameObject.name} was pressed at {timeSinceStart:F2} seconds from the start.");

        StartCoroutine(ChangeColorTemporarily(Color.green, 2.0f));
    }

    private IEnumerator ChangeColorTemporarily(Color newColor, float duration)
    {
        // Change the cube's color
        cubeRenderer.material.color = newColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert the cube's color back to the original
        cubeRenderer.material.color = originalColor;
    }
}
