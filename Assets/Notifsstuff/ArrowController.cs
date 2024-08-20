using UnityEngine;
using System.Collections;

public class ArrowController : MonoBehaviour
{
    private Renderer arrowRenderer;
    private Coroutine blinkCoroutine;
    private bool isBlinking = false;
    public float blinkDuration = 5f; //duration to blink for each notification
    private float[] blinkTimes = new float[]
    {
        60f,   // 1:00
        105f,  // 1:45
        150f,  // 2:30
        185f,  // 3:05
        235f   // 3:55
    };

    private Vector3[] directions = new Vector3[]
    {
        new Vector3(-1, -1, 0), // SW
        new Vector3(0, 1, 0),   // N
        new Vector3(1, 0, 0),   // E
        new Vector3(1, 1, 0),   // NE
        new Vector3(-1, 0, 0)   // W
    };

    private float[] rotationAngles = new float[]
    {
        -135f, // SW
        0f,    // N
        90f,   // E
        45f,   // NE
        -90f   // W
    };

    private void Start()
    {
        arrowRenderer = GetComponent<Renderer>();
        arrowRenderer.enabled = false; // Start with the arrow invisible
        StartCoroutine(BlinkAtScheduledTimes());
    }

    private void Update()
    {
        // Handle input for different directions
        if (Input.GetKeyDown(KeyCode.O))
        {
            SetArrowDirection(rotationAngles[1]); // North
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            SetArrowDirection(rotationAngles[0]); // South
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            SetArrowDirection(rotationAngles[2]); // East
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            SetArrowDirection(rotationAngles[4]); // West
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SetArrowDirection(rotationAngles[3]); // North-East
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            SetArrowDirection(rotationAngles[4]); // North-West
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            SetArrowDirection(rotationAngles[5]); // South-East
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            SetArrowDirection(rotationAngles[6]); // South-West
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopBlinking();
        }
    }

    private void SetArrowDirection(float yRotation)
    {
        float xRotation = 70; // Constant downward tilt angle
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        // Start the blinking coroutine if not already blinking
        if (!isBlinking)
        {
            blinkCoroutine = StartCoroutine(BlinkArrow());
        }
    }

    private IEnumerator BlinkArrow()
    {
        isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            arrowRenderer.enabled = !arrowRenderer.enabled; // Toggle visibility
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 0.5f;
        }

        arrowRenderer.enabled = false; // Ensure the arrow is invisible after blinking
        isBlinking = false;
    }

    private void StopBlinking()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        arrowRenderer.enabled = false;
        isBlinking = false;
    }

    private IEnumerator BlinkAtScheduledTimes()
    {
        float startTime = Time.time;

        for (int i = 0; i < blinkTimes.Length; i++)
        {
            float targetTime = startTime + blinkTimes[i];
            Vector3 direction = directions[i];
            float angle = rotationAngles[i];

            // Wait until the target time is reached
            while (Time.time < targetTime)
            {
                yield return null;
            }

            SetArrowDirection(angle);
            yield return new WaitForSeconds(blinkDuration); // Keep arrow direction for 1 second before starting to blink
        }

        // After the last blink time, you can choose to stop blinking or do something else
        StopBlinking();
    }
}

