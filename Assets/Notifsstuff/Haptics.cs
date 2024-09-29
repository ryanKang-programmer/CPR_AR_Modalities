using UnityEngine;
using Bhaptics.SDK2;
using System.Collections;

public class Haptics : MonoBehaviour
{
    public Transform[] surroundingPoints; // Array of 8 surrounding points
    public float intensity = 0.7f; // Intensity of the haptic feedback (0 to 1)

    private string currentHapticEvent = ""; // Keep track of the current active haptic event
    private Transform selectedPoint; // The currently selected point
    public Transform spherePosition;
    private string[] hapticEvents = { "swest", "seast", "neast", "nwest", "south", "east", "west" };
    private float startTime;

    void Start()
    {
        Debug.Log("Start called");
        // StartCoroutine(TriggerTimedHaptics());
        //TriggerHapticEvent("south");
        startTime = Time.time;
    }

    void Update()
    {
        Debug.Log("Update called");
        CheckPointSelection();
        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine(PlayRandomHapticEvent(5));
        }
    }

    void CheckPointSelection()
    {
        if (Input.GetKeyDown(KeyCode.X)) // South key
        {
            selectedPoint = surroundingPoints[3]; // Assuming the South point is at index 3
            Debug.Log("South (N) selected");
            TriggerHapticEvent("south");
            CastRay(); // Call CastRay to check for intersections
        }
    }

    void CastRay()
    {
        RaycastHit hit;
        Vector3 direction = (spherePosition.position - selectedPoint.position).normalized; // Ray direction towards the center sphere

        if (Physics.Raycast(selectedPoint.position, direction, out hit))
        {
            string sectionName = hit.collider.gameObject.name;
            Debug.Log($"Hit section: {sectionName}");

            // Trigger the corresponding haptic event if a new section is hit
            if (sectionName != currentHapticEvent)
            {
                StopCurrentHapticEvent();
                TriggerHapticEvent(sectionName);
                currentHapticEvent = sectionName;
            }
        }
        else
        {
            StopCurrentHapticEvent();
        }
    }

    void StopCurrentHapticEvent()
    {
        if (!string.IsNullOrEmpty(currentHapticEvent))
        {
            StopHapticEvent(currentHapticEvent);
            currentHapticEvent = "";
        }
    }

    void TriggerHapticEvent(string eventName)
    {
        BhapticsLibrary.Play(eventName); // Trigger the haptic event
        Debug.Log("Haptic event triggered: " + eventName);
    }

    void StopHapticEvent(string eventName)
    {
        BhapticsLibrary.StopByEventId(eventName); // Stop the haptic event
    }

    // IEnumerator TriggerTimedHaptics()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(60f);
    //         Debug.Log("Triggering South Haptic Event");
    //         TriggerHapticEvent("south");
    //         yield return new WaitForSeconds(300f); // Adjust the wait time as needed
    //     }
    // }

    IEnumerator PlayRandomHapticEvent(int eventCount)
    {
        for (int i = 0; i < eventCount; i++)
        {
            // Randomly select an event name from the array
            string selectedEvent = hapticEvents[Random.Range(0, hapticEvents.Length)];
            
            // Randomly select a duration between 30 and 50 seconds
            float interval = Random.Range(30f, 50f);
            float duration = 5f;
            Debug.Log($"Playing haptic event: {selectedEvent} for {duration} seconds");
            float eventStartTime = Time.time - startTime; // Calculate elapsed time from start
            Debug.Log($"Playing haptic event: {selectedEvent} for {duration} seconds. Time from start: {eventStartTime} seconds");

            TriggerHapticEvent(selectedEvent); // Play the selected haptic event

            // Wait for the duration of the haptic event
            yield return new WaitForSeconds(interval);
            
            StopHapticEvent(selectedEvent); // Stop the haptic event after the duration

            // Optional: Wait for a short interval before the next event
            yield return new WaitForSeconds(2f); // Adjust the wait time between events as needed
        }
    }
}
