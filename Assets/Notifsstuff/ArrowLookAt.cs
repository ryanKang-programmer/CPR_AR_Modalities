using UnityEngine;
using System.Collections;

public class ArrowLookAt : MonoBehaviour
{
    public Transform[] objectsToPointAt;  // Array of buttons
    public Transform arrow;               // The arrow GameObject (head)
    
    private Transform targetObject;       // The current object to point at
    public Vector3 rotationOffset = new Vector3(90, 0, 0); 
    private bool isCoroutineRunning = false;  

    void Start()
    {
        // //make the arrow invisible at the start
        // arrow.gameObject.SetActive(false);
    }

    void Update()
    {

        // Start the random pointing coroutine when the "v" key is pressed
        if (Input.GetKeyDown(KeyCode.V) && !isCoroutineRunning)
        {
            Debug.Log("start");
            StartCoroutine(PointAtRandomTargets(5));  // Loop 5 times
        }

        // Check for specific button presses and select a target
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            targetObject = objectsToPointAt[0];  //NE
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            targetObject = objectsToPointAt[1];  //NW
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            targetObject = objectsToPointAt[2];  //SE
        }
        // Add more key checks for other objects if needed
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            targetObject = objectsToPointAt[3];  //SW
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            targetObject = objectsToPointAt[4];  //S
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            targetObject = objectsToPointAt[5];  //E
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            targetObject = objectsToPointAt[6];  //W
        }

        // If a target is set, make the arrow point to it
        if (targetObject != null)
        {
            Vector3 direction = (targetObject.position - arrow.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            Quaternion rotateValue = Quaternion.RotateTowards(arrow.rotation, rotation, 60.0f * Time.deltaTime);
            //arrow.rotation = rotateValue;


            // Flip the arrow by 180 degrees on the Y axis to make the arrow head point toward the target
            //Quaternion flippedRotation = rotation * Quaternion.Euler(0, 180, 0);

            //Quaternion lookRotation = Quaternion.LookRotation(direction);

            //Quaternion adjustedRotation = lookRotation * Quaternion.Euler(rotationOffset);

            arrow.rotation = Quaternion.Slerp(arrow.rotation, rotateValue, Time.deltaTime * 30f);
        }
    }

    // Coroutine to randomly pick targets and point the arrow at them
    IEnumerator PointAtRandomTargets(int numberOfLoops)
    {
        isCoroutineRunning = true;

        for (int i = 0; i < numberOfLoops; i++)
        {
            // Pick a random target from the objectsToPointAt array
            Transform randomTarget = objectsToPointAt[Random.Range(0, objectsToPointAt.Length)];
            // // Make the arrow visible
            // arrow.gameObject.SetActive(true);

            // Make the arrow point to the randomly selected target
            if (randomTarget != null)
            {
                Vector3 direction = (randomTarget.position - arrow.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Quaternion adjustedRotation = lookRotation * Quaternion.Euler(rotationOffset);
                arrow.rotation = adjustedRotation;
                Debug.Log("update!");
            }

            // // Keep the arrow visible for 5 seconds
            // yield return new WaitForSeconds(5f);

            // // Hide the arrow after 5 seconds
            // arrow.gameObject.SetActive(false);

            // Wait for a random interval between 30 and 50 seconds before picking the next target
            float randomInterval = Random.Range(30f, 50f);
            yield return new WaitForSeconds(randomInterval);
        }

        // After looping, reset coroutine state
        isCoroutineRunning = false;
    }
}
