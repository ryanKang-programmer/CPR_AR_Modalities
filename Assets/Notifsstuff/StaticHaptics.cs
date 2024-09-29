using UnityEngine;
using Bhaptics.SDK2;
using System.Collections;
public class StaticHaptics : MonoBehaviour
{
    //public Transform mainPoint; // Sphere object
    public Transform[] surroundingPoints; // Array of 8 surrounding points
    //public GameObject[] planeSections; // Array of the 32 plane sections (8 per plane)
    //public Transform userTransform; // Main camera or user's transform
    public float intensity = 0.7f; // Intensity of the haptic feedback (0 to 1)

    private string currentHapticEvent = ""; // Keep track of the current active haptic event
    private Transform selectedPoint; // The currently selected point

    void Start()
    {
        //TriggerHapticEvent("fa1");
        Debug.Log("Start called");
        // Start the timed haptic feedback coroutine
        StartCoroutine(TriggerTimedHaptics());
    }

    void Update()
    {
        Debug.Log("Update called");

        // Check for point selection input
        CheckPointSelection();

        // // Continuously cast a ray if a point is selected
        // if (selectedPoint != null)
        // {
        //     CastRay();
        // }

    }

    void CheckPointSelection()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            selectedPoint = surroundingPoints[0]; 
            Debug.Log("North (O) selected");
            TriggerHapticEvent("fb2");
            TriggerHapticEvent("fb3");
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        {
            selectedPoint = surroundingPoints[1]; 
            Debug.Log("North-East (P) selected");
            TriggerHapticEvent("fa4");
        }
        else if (Input.GetKeyDown(KeyCode.Q)) 
        {
            selectedPoint = surroundingPoints[2]; 
            Debug.Log("North-West (I) selected");
            TriggerHapticEvent("fa1");
        }
        else if (Input.GetKeyDown(KeyCode.X)) 
        {
            selectedPoint = surroundingPoints[3]; 
            Debug.Log("South (N) selected");
            TriggerHapticEvent("bb2");
            TriggerHapticEvent("bb3");
        }
        else if (Input.GetKeyDown(KeyCode.C)) 
        {
            selectedPoint = surroundingPoints[4]; 
            Debug.Log("South-East (M) selected");
            TriggerHapticEvent("ba4");
        }
        else if (Input.GetKeyDown(KeyCode.Z)) 
        {
            selectedPoint = surroundingPoints[5]; 
            Debug.Log("South-West (B) selected");
            TriggerHapticEvent("ba1");
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            selectedPoint = surroundingPoints[6]; 
            Debug.Log("East (L) selected");
            TriggerHapticEvent("fe4");
        }
        else if (Input.GetKeyDown(KeyCode.A)) 
        {
            selectedPoint = surroundingPoints[7]; 
            Debug.Log("West (J) selected");
            TriggerHapticEvent("fe1");
        }
    }

    void StopCurrentHapticEvent()
    {
        // Stop the current haptic event if one is active
        if (!string.IsNullOrEmpty(currentHapticEvent))
        {
            StopHapticEvent(currentHapticEvent);
            currentHapticEvent = "";
        }
    }

    // void CastRay()
    // {
    //     RaycastHit hit;

    //     // Calculate the direction based on the user's current facing direction
    //     Vector3 direction = (mainPoint.position - selectedPoint.position).normalized;

    //     // Perform the raycast
    //     if (Physics.Raycast(selectedPoint.position, direction, out hit))
    //     {
    //         string sectionName = hit.collider.gameObject.name;

    //         // Trigger the corresponding haptic event if the ray hits a new section
    //         if (sectionName != currentHapticEvent)
    //         {
    //             // Stop the previous haptic event
    //             if (!string.IsNullOrEmpty(currentHapticEvent))
    //             {
    //                 StopHapticEvent(currentHapticEvent);
    //             }

    //             // Trigger the new haptic event
    //             TriggerHapticEvent(sectionName);
    //             currentHapticEvent = sectionName;
    //         }
    //     }
    //     else
    //     {
    //         // If no hit detected, stop the current haptic event
    //         if (!string.IsNullOrEmpty(currentHapticEvent))
    //         {
    //             StopHapticEvent(currentHapticEvent);
    //             currentHapticEvent = "";
    //         }
    //     }
    // }

    void TriggerHapticEvent(string eventName)
    {
        // Trigger the haptic event with the specified intensity using bHaptics SDK
        //BhapticsLibrary.PlayParam(eventName, intensity: intensity, duration: 1f);
        BhapticsLibrary.Play(eventName);
        Debug.Log("buzzer on");
    }

    void StopHapticEvent(string eventName)
    {
        // Stop the haptic event using bHaptics SDK
        BhapticsLibrary.StopByEventId(eventName);
    }

    // IEnumerator TriggerTimedHaptics()
    // {
    //     while (true)
    //     {
    //         yield return new WaitForSeconds(60f);
    //         TriggerHapticEvent("south");
    //         //TriggerHapticEvent("bb3"); // South
    //         yield return new WaitForSeconds(105f);
    //         TriggerHapticEvent("s-e"); // South-East
    //         yield return new WaitForSeconds(150f);
    //         TriggerHapticEvent("n-e"); // North-East
    //         yield return new WaitForSeconds(185f);
    //         TriggerHapticEvent("east"); // East
    //         yield return new WaitForSeconds(235f);
    //         TriggerHapticEvent("west");
    //         //TriggerHapticEvent("fb3"); // North

    //         // Wait for the next cycle to start
    //         yield return new WaitForSeconds(300f); // Adjust the wait time as needed
    //     }
    // }

    IEnumerator TriggerTimedHaptics()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f);
            Debug.Log("Triggering South Haptic Event");
            TriggerHapticEvent("south");

            yield return new WaitForSeconds(45f); // 105f total time (1:45)
            Debug.Log("Triggering South-East Haptic Event");
            TriggerHapticEvent("s-e");

            yield return new WaitForSeconds(45f); // 150f total time (2:30)
            Debug.Log("Triggering North-East Haptic Event");
            TriggerHapticEvent("n-e");

            yield return new WaitForSeconds(35f); // 185f total time (3:05)
            Debug.Log("Triggering East Haptic Event");
            TriggerHapticEvent("east");

            yield return new WaitForSeconds(50f); // 235f total time (3:55)
            Debug.Log("Triggering West Haptic Event");
            TriggerHapticEvent("west");

            // Wait for the next cycle to start
            yield return new WaitForSeconds(300f); // Adjust the wait time as needed
        }
    }

}
