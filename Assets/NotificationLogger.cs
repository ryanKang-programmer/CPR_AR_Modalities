using System.Collections;
using UnityEngine;

public class NotificationLogger : MonoBehaviour
{
    public GameObject perceptionButton; // Assign your perception button GameObject here
    public GameObject[] directionalButtons; // Assign your directional button GameObjects here
    private float[] notificationTimes = new float[]
    {
        0f,    // 0:00
        60f,   // 1:00
        105f,  // 1:45
        150f,  // 2:30
        185f,  // 3:05
        235f,  // 3:55
        285f,  // 4:45
        320f,  // 5:20
        360f,  // 6:00
        400f,  // 6:40
        450f,  // 7:30
        490f,  // 8:10
        535f,  // 8:55
        565f,  // 9:25
        605f,  // 10:05
        675f   // 11:15
    };

    private bool isWaitingForPerceptionButton = false;
    private bool isWaitingForDirectionButton = false;
    private float perceptionStartTime;
    private float directionStartTime;

    private void Start()
    {
        StartCoroutine(NotificationRoutine());
    }

    private IEnumerator NotificationRoutine()
    {
        foreach (float notificationTime in notificationTimes)
        {
            yield return new WaitForSeconds(notificationTime - Time.time);
            Debug.Log($"Notification triggered at {notificationTime} seconds.");

            // Start timing for perception button press
            isWaitingForPerceptionButton = true;
            perceptionStartTime = Time.time;

            // Wait for the perception button to be pressed
            while (isWaitingForPerceptionButton)
            {
                yield return null;
            }

            // Start timing for direction button press
            isWaitingForDirectionButton = true;
            directionStartTime = Time.time;

            // Wait for a directional button to be pressed
            while (isWaitingForDirectionButton)
            {
                yield return null;
            }
        }
    }

    // private void Update()
    // {
    //     // Check if the perception button is pressed
    //     if (isWaitingForPerceptionButton && perceptionButton.GetComponent<ButtonPressDetector>().isPressed)
    //     {
    //         isWaitingForPerceptionButton = false;
    //         float timeTaken = Time.time - perceptionStartTime;
    //         Debug.Log($"Perception button pressed after {timeTaken} seconds.");
    //     }

    //     // Check if any directional button is pressed
    //     if (isWaitingForDirectionButton)
    //     {
    //         foreach (GameObject button in directionalButtons)
    //         {
    //             if (button.GetComponent<ButtonPressDetector>().isPressed)
    //             {
    //                 isWaitingForDirectionButton = false;
    //                 float timeTaken = Time.time - directionStartTime;
    //                 Debug.Log($"Directional button {button.name} pressed after {timeTaken} seconds.");
    //                 break;
    //             }
    //         }
    //     }
    // }
}
