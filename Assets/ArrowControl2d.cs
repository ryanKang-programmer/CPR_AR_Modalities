using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ArrowControl2d : MonoBehaviour
{
    public Image arrowImage; // Assign the arrow image in the inspector
    public float visibleDuration = 5f; // Duration the arrow stays visible
    public bool enableBlinking = false; // If true, the arrow will blink
    public float blinkInterval = 0.5f; // Interval between blinks

    private float[] notificationTimes = new float[]
    {
        60f,   // 1:00
        105f,  // 1:45
        150f,  // 2:30
        185f,  // 3:05
        235f   // 3:55
    };

    private Vector3[] rotations = new Vector3[]
    {
        new Vector3(0f, 0f, -135f), // SW
        new Vector3(0f, 0f, 135f),  // NW
        new Vector3(0f, 0f, 0f),    // E
        new Vector3(0f, 0f, 45f),   // NE
        new Vector3(0f, 0f, 180f)   // W
    };

    private void Start()
    {
        arrowImage.enabled = false; // Start with the arrow invisible
        StartCoroutine(DisplayArrowAtScheduledTimes());
    }

    private IEnumerator DisplayArrowAtScheduledTimes()
    {
        float startTime = Time.time;

        for (int i = 0; i < notificationTimes.Length; i++)
        {
            float targetTime = startTime + notificationTimes[i];
            Vector3 rotation = rotations[i];

            // Wait until the target time is reached
            while (Time.time < targetTime)
            {
                yield return null;
            }

            // Set the arrow's rotation
            transform.rotation = Quaternion.Euler(rotation);

            // Make the arrow visible
            arrowImage.enabled = true;

            if (enableBlinking)
            {
                StartCoroutine(BlinkArrow());
            }
            else
            {
                yield return new WaitForSeconds(visibleDuration);
                arrowImage.enabled = false;
            }
        }
    }

    private IEnumerator BlinkArrow()
    {
        float elapsedTime = 0f;

        while (elapsedTime < visibleDuration)
        {
            arrowImage.enabled = !arrowImage.enabled; // Toggle visibility
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }

        arrowImage.enabled = false;
    }
}
